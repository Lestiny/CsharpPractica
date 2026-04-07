using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CarRental
{
    public class CarRentalViewModel : INotifyPropertyChanged
    {
        private readonly RentalService _rentalService = new();
        private CarModel? _selectedCar;
        private RentalModel? _selectedRental;
        private string _clientName = "";
        private DateTime _startDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today.AddDays(1);
        private bool _isProcessing;
        private string _statusMessage = "Готово к оформлению";

        public ObservableCollection<CarModel> AvailableCars { get; } = new();
        public ObservableCollection<RentalModel> ActiveRentals { get; } = new();

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

        public CarRentalViewModel()
        {
            AvailableCars.Add(new CarModel { Make = "Lada", Model = "Granta", PricePerDay = 70, FleetSize = 3 });
            AvailableCars.Add(new CarModel { Make = "Hyundai", Model = "Solaris", PricePerDay = 95, FleetSize = 2 });
            AvailableCars.Add(new CarModel { Make = "Audi", Model = "A4 B5", PricePerDay = 120, FleetSize = 2 });
            AvailableCars.Add(new CarModel { Make = "Toyota", Model = "Camry", PricePerDay = 170, FleetSize = 1 });
            AvailableCars.Add(new CarModel { Make = "BMW", Model = "M5", PricePerDay = 420, FleetSize = 1 });

            RentCarCommand = new AsyncRelayCommand(RentCarAsync, CanRentCar);
            ReturnCarCommand = new RelayCommand(_ => ReturnCar(), _ => CanReturnCar());
        }

        private bool CanRentCar()
        {
            if (IsProcessing || SelectedCar == null) return false;
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

        private bool CanReturnCar() => !IsProcessing && SelectedRental != null;

        private void ReturnCar()
        {
            if (SelectedRental == null) return;
            _rentalService.ReturnRental(SelectedRental, ActiveRentals);
            SelectedRental = null;
            StatusMessage = "Автомобиль возвращен";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
