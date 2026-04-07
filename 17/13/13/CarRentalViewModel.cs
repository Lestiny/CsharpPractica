using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CarRental
{
    public class CarRentalViewModel : INotifyPropertyChanged, IDisposable
    {
        private readonly RentalService _rentalService = new();
        private readonly RentalDataStore _store;
        private readonly AuthService _authService;
        private readonly ChatPipeService _chatPipe;
        private readonly BookingNotificationService _notificationService;
        private readonly FileSystemWatcher _rentalWatcher;

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

        public CarRentalViewModel()
        {
            var appData = Path.Combine(AppContext.BaseDirectory, "Data");
            _store = new RentalDataStore(appData);
            _authService = new AuthService(_store);
            _chatPipe = new ChatPipeService("car-rental-chat-pipe");
            _notificationService = new BookingNotificationService("CarRentalBookingMap");

            LoadStateFromFile();

            RentCarCommand = new AsyncRelayCommand(RentCarAsync, CanRentCar);
            ReturnCarCommand = new RelayCommand(_ => ReturnCar(), _ => CanReturnCar());
            RegisterCommand = new RelayCommand(_ => RegisterUser());
            LoginCommand = new RelayCommand(_ => LoginUser());
            LogoutCommand = new RelayCommand(_ => LogoutUser(), _ => IsAuthenticated);
            SendChatCommand = new AsyncRelayCommand(SendChatAsync, CanSendChat);

            _chatPipe.MessageReceived += OnChatMessageReceived;
            _notificationService.NotificationReceived += OnNotificationReceived;

            _rentalWatcher = new FileSystemWatcher(appData, "rental.json")
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };
            _rentalWatcher.Changed += (_, _) => ReloadRentalsFromWatcher();
            _rentalWatcher.EnableRaisingEvents = true;

            ChatMessages.Add("Система: чат запущен (Named Pipes).");
        }

        private bool CanRentCar()
        {
            if (!IsAuthenticated || IsProcessing || SelectedCar == null) return false;
            if (string.IsNullOrWhiteSpace(ClientName)) return false;
            if (EndDate.Date < StartDate.Date) return false;
            return true;
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
                SaveStateToFile();
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

        private bool CanReturnCar() => IsAuthenticated && !IsProcessing && SelectedRental != null;

        private void ReturnCar()
        {
            if (SelectedRental == null) return;
            _rentalService.ReturnRental(SelectedRental, ActiveRentals);
            SaveStateToFile();
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
            ClientName = user.Username;
            AuthPassword = "";
            StatusMessage = $"Выполнен вход: {user.Username}";
        }

        private void LogoutUser()
        {
            CurrentUser = null;
            StatusMessage = "Вы вышли из системы";
        }

        private bool CanSendChat() => IsAuthenticated && !string.IsNullOrWhiteSpace(ChatInput);

        private async Task SendChatAsync()
        {
            var user = CurrentUser?.Username ?? "anonymous";
            var msg = $"{DateTime.Now:HH:mm:ss} {user}: {ChatInput.Trim()}";
            ChatMessages.Add(msg);
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

        private void LoadStateFromFile()
        {
            var state = _store.LoadRentalState();
            AvailableCars.Clear();
            ActiveRentals.Clear();
            foreach (var car in state.Cars) AvailableCars.Add(car);
            foreach (var rental in state.Rentals) ActiveRentals.Add(rental);
        }

        private void SaveStateToFile()
        {
            _store.SaveRentalState(new RentalState
            {
                Cars = AvailableCars.ToList(),
                Rentals = ActiveRentals.ToList()
            });
        }

        private void ReloadRentalsFromWatcher()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var state = _store.LoadRentalState();
                    ActiveRentals.Clear();
                    foreach (var rental in state.Rentals)
                        ActiveRentals.Add(rental);
                });
            }
            catch
            {

            }
        }

        public void Dispose()
        {
            _rentalWatcher.Dispose();
            _chatPipe.Dispose();
            _notificationService.Dispose();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
