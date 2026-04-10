using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace CarRental
{
    public class CarRentalViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly RentalService _rentalService = new();
        private readonly RentalDataStore _store;
        private readonly AuthService _authService;
        private readonly ChatPipeService _chatPipe;
        private readonly BookingNotificationService _notificationService;

        private readonly CarRentalDbContext _context;
        private readonly CarRepository _carRepository;
        private readonly RentalRepository _rentalRepository;

        private CarModel? _selectedCar;
        private RentalModel? _selectedRental;
        private string _clientName = "";
        private string _authUsername = "";
        private string _authPassword = "";
        private UserRole _selectedRole = UserRole.Client;
        private UserAccount? _currentUser;
        private string _chatInput = "";
        private DateTime _startDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today.AddDays(1);
        private bool _isProcessing;
        private string _statusMessage = "Готово к оформлению";

        public ObservableCollection<CarModel> Cars { get; } = new();
        public ObservableCollection<CarModel> AvailableCars { get; } = new();
        public ObservableCollection<RentalModel> ActiveRentals { get; } = new();
        public ObservableCollection<string> ChatMessages { get; } = new();

        public CarModel? SelectedCar
        {
            get => _selectedCar;
            set
            {
                if (_selectedCar == value) return;
                _selectedCar = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public RentalModel? SelectedRental
        {
            get => _selectedRental;
            set
            {
                if (_selectedRental == value) return;
                _selectedRental = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                if (_clientName == value) return;
                _clientName = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string AuthUsername
        {
            get => _authUsername;
            set
            {
                if (_authUsername == value) return;
                _authUsername = value;
                OnPropertyChanged();
            }
        }

        public string AuthPassword
        {
            get => _authPassword;
            set
            {
                if (_authPassword == value) return;
                _authPassword = value;
                OnPropertyChanged();
            }
        }

        public UserRole SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (_selectedRole == value) return;
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        public UserAccount? CurrentUser
        {
            get => _currentUser;
            private set
            {
                if (_currentUser == value) return;
                _currentUser = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsAuthenticated));
                OnPropertyChanged(nameof(CurrentUserDisplay));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public bool IsAuthenticated => CurrentUser != null;

        public string CurrentUserDisplay =>
            CurrentUser == null
                ? "Не авторизован"
                : $"{CurrentUser.Username} ({(CurrentUser.Role == UserRole.Client ? "Клиент" : "Менеджер")})";

        public string ChatInput
        {
            get => _chatInput;
            set
            {
                if (_chatInput == value) return;
                _chatInput = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate == value) return;
                _startDate = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate == value) return;
                _endDate = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            private set
            {
                if (_isProcessing == value) return;
                _isProcessing = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            private set
            {
                if (_statusMessage == value) return;
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand RentCarCommand { get; }
        public ICommand ReturnCarCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand SendChatCommand { get; }

        public CarRentalViewModel(UserAccount user, CarRentalDbContext context)
        {
            var appData = Path.Combine(AppContext.BaseDirectory, "Data");
            _store = new RentalDataStore(appData);
            _authService = new AuthService(_store);
            _chatPipe = new ChatPipeService("car-rental-chat-pipe");
            _notificationService = new BookingNotificationService("CarRentalBookingMap");
            _context = context;
            _carRepository = new CarRepository(_context);
            _rentalRepository = new RentalRepository(_context);

            _ = LoadStateFromDatabaseAsync();
            CurrentUser = user;
            _store.SaveLastUsername(user.Username);
            ClientName = user.Username;

            RentCarCommand = new AsyncRelayCommand(RentCarAsync, CanRentCar);
            ReturnCarCommand = new AsyncRelayCommand(ReturnCarAsync, CanReturnCar);
            RegisterCommand = new RelayCommand(_ => RegisterUser());
            LoginCommand = new RelayCommand(_ => LoginUser());
            LogoutCommand = new RelayCommand(_ => LogoutUser(), _ => IsAuthenticated);
            SendChatCommand = new AsyncRelayCommand(SendChatAsync, CanSendChat);

            _chatPipe.MessageReceived += OnChatMessageReceived;
            _notificationService.NotificationReceived += OnNotificationReceived;

            ChatMessages.Add("Система: чат запущен.");
        }

        private bool CanRentCar()
        {
            if (!IsAuthenticated || IsProcessing || SelectedCar == null) return false;
            if (string.IsNullOrWhiteSpace(ClientName)) return false;
            if (EndDate.Date < StartDate.Date) return false;
            return true;
        }

        private void UpdateCarsAvailability()
        {
            foreach (var car in Cars)
            {
                var active = ActiveRentals.Count(r => r.CarName == car.DisplayName);
                car.OccupiedCount = active;
                car.IsFree = active < car.FleetSize;
            }
        }

        private async Task RentCarAsync()
        {
            try
            {
                IsProcessing = true;
                StatusMessage = "Обрабатывается";

                var rental = await _rentalService.CreateRentalAsync(
                    SelectedCar!,
                    ClientName,
                    StartDate,
                    EndDate,
                    ActiveRentals);

                ActiveRentals.Add(rental);
                UpdateCarsAvailability();

                var carEntity = await _context.Cars
                    .FirstAsync(c => c.Make == SelectedCar!.Make && c.Model == SelectedCar.Model);

                _context.Rentals.Add(new RentalEntity
                {
                    CarId = carEntity.Id,
                    ClientName = rental.ClientName,
                    StartDate = rental.StartDate,
                    EndDate = rental.EndDate,
                    PricePerDay = rental.PricePerDay
                });

                await _context.SaveChangesAsync();

                var who = CurrentUser?.Username ?? ClientName;
                _notificationService.Publish($"Новая бронь: {rental.CarName}, клиент {who}");
                ClientName = "";
                StatusMessage = "Заявка успешно оформлена";
            }
            catch (Exception ex)
            {
                StatusMessage = "Ошибка обработки заявки";
                MessageBox.Show(ex.Message, "Аренда автомобиля", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private bool CanReturnCar()
        {
            if (!IsAuthenticated || IsProcessing || SelectedRental == null) return false;
            return CanReturnRental(SelectedRental);
        }

        private bool CanReturnRental(RentalModel rental)
        {
            var user = CurrentUser;
            if (user == null) return false;
            if (user.Role == UserRole.Manager) return true;
            return string.Equals(rental.ClientName, user.Username, StringComparison.OrdinalIgnoreCase);
        }

        private async Task ReturnCarAsync()
        {
            if (SelectedRental == null) return;
            if (!CanReturnRental(SelectedRental))
            {
                MessageBox.Show("Возврат может выполнить только пользователь, который оформил аренду, или менеджер.",
                    "Возврат автомобиля", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selected = SelectedRental;
            var rentalEntity = await _context.Rentals
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r =>
                    r.Car!.Make + " " + r.Car.Model == selected.CarName &&
                    r.ClientName == selected.ClientName &&
                    r.StartDate == selected.StartDate &&
                    r.EndDate == selected.EndDate);

            if (rentalEntity != null)
            {
                _context.Rentals.Remove(rentalEntity);
                await _context.SaveChangesAsync();
            }

            _rentalService.ReturnRental(SelectedRental, ActiveRentals);
            UpdateCarsAvailability();
            SelectedRental = null;
            StatusMessage = "Автомобиль возвращен";
        }

        private void RegisterUser()
        {
            if (_authService.Register(AuthUsername, AuthPassword, SelectedRole, out var message))
            {
                StatusMessage = message;
                AuthPassword = "";
            }
            else
            {
                MessageBox.Show(message, "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoginUser()
        {
            var user = _authService.Login(AuthUsername, AuthPassword);
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль.", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CurrentUser = user;
            _store.SaveLastUsername(user.Username);
            ClientName = user.Username;
            AuthPassword = "";
            StatusMessage = $"Выполнен вход: {user.Username}";
        }

        private void LogoutUser()
        {
            CurrentUser = null;
            _store.SaveLastUsername(null);
            AuthPassword = "";
            StatusMessage = "Вы вышли из системы";
        }

        public void Logout() => LogoutUser();

        private bool CanSendChat() => IsAuthenticated && !string.IsNullOrWhiteSpace(ChatInput);

        private async Task SendChatAsync()
        {
            var user = CurrentUser?.Username ?? "anonymous";
            var msg = $"{DateTime.Now:HH:mm:ss} {user}: {ChatInput.Trim()}";
            await _chatPipe.SendAsync(msg);
            ChatInput = "";
        }

        private void OnChatMessageReceived(string message)
        {
            Application.Current.Dispatcher.Invoke(() => ChatMessages.Add(message));
        }

        private void OnNotificationReceived(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (message.StartsWith("Новая бронь:", StringComparison.Ordinal))
                    StatusMessage = message;
            });
        }

        private async Task LoadStateFromDatabaseAsync()
        {
            try
            {
                var cars = await _carRepository.GetAllAsync();
                var rentals = await _rentalRepository.GetAllAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Cars.Clear();
                    AvailableCars.Clear();
                    ActiveRentals.Clear();

                    foreach (var car in cars)
                    {
                        var model = new CarModel
                        {
                            Make = car.Make,
                            Model = car.Model,
                            PricePerDay = car.PricePerDay,
                            FleetSize = car.FleetSize
                        };
                        Cars.Add(model);
                        AvailableCars.Add(model);
                    }

                    foreach (var r in rentals)
                    {
                        var carName = r.Car == null ? "—" : $"{r.Car.Make} {r.Car.Model}".Trim();
                        ActiveRentals.Add(new RentalModel
                        {
                            CarName = carName,
                            ClientName = r.ClientName,
                            StartDate = r.StartDate,
                            EndDate = r.EndDate,
                            PricePerDay = r.PricePerDay
                        });
                    }

                    UpdateCarsAvailability();
                });
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    StatusMessage = "Ошибка загрузки данных из базы";
                    MessageBox.Show(ex.Message, "Загрузка данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                });
            }
        }

        public void Dispose()
        {
            _chatPipe.Dispose();
            _notificationService.Dispose();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
