using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace CarRental
{
    public class BookingDialogViewModel : INotifyPropertyChanged
    {
        private readonly Booking? _editingBooking;
        private readonly ObservableCollection<Booking> _bookings;
        private readonly ObservableCollection<Car> _cars;
        private readonly ICollectionView _carsView;

        private Car? _selectedCar;
        private DateTime? _startDate = DateTime.Today;
        private DateTime? _endDate = DateTime.Today.AddDays(1);
        private string _clientName = "";
        private int _filterClassIndex;
        private int _minPassengerSeats;
        private bool _onlyWithAvailableFleet = true;

        public BookingDialogViewModel(
            ObservableCollection<Car> cars,
            ObservableCollection<Booking> bookings,
            Booking? editingBooking = null)
        {
            _cars = cars;
            _bookings = bookings;
            _editingBooking = editingBooking;
            _carsView = CollectionViewSource.GetDefaultView(cars);
            _carsView.Filter = FilterCar;
            bookings.CollectionChanged += Bookings_CollectionChanged;

            if (editingBooking != null)
            {
                _clientName = editingBooking.ClientName;
                _startDate = editingBooking.StartDate;
                _endDate = editingBooking.EndDate;
                SelectedCar = cars.FirstOrDefault(c => c.DisplayName == editingBooking.CarName);
            }

            UpdateCarsAvailability();
            RefreshCostDisplay();
        }

        public ICollectionView CarsView => _carsView;

        public Car? SelectedCar
        {
            get => _selectedCar;
            set
            {
                if (_selectedCar != value)
                {
                    _selectedCar = value;
                    OnPropertyChanged();
                    RefreshCostDisplay();
                }
            }
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                    RefreshCostDisplay();
                }
            }
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged();
                    RefreshCostDisplay();
                }
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                if (_clientName != value)
                {
                    _clientName = value;
                    OnPropertyChanged();
                }
            }
        }


        public int FilterClassIndex
        {
            get => _filterClassIndex;
            set
            {
                if (_filterClassIndex != value)
                {
                    _filterClassIndex = value;
                    OnPropertyChanged();
                    ApplyFilter();
                }
            }
        }

        public int[] SeatThresholdOptions { get; } = { 0, 4, 5, 7 };

        public int MinPassengerSeats
        {
            get => _minPassengerSeats;
            set
            {
                if (_minPassengerSeats != value)
                {
                    _minPassengerSeats = value;
                    OnPropertyChanged();
                    ApplyFilter();
                }
            }
        }

        public bool OnlyWithAvailableFleet
        {
            get => _onlyWithAvailableFleet;
            set
            {
                if (_onlyWithAvailableFleet != value)
                {
                    _onlyWithAvailableFleet = value;
                    OnPropertyChanged();
                    ApplyFilter();
                }
            }
        }


        public string RentalCostDisplay { get; private set; } = "—";

        public int RentalDays
        {
            get
            {
                var start = StartDate?.Date ?? DateTime.Today;
                var end = EndDate?.Date ?? start;
                if (end < start) return 0;
                return Math.Max(1, (end - start).Days + 1);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Validate(out string? error)
        {
            error = null;
            if (SelectedCar == null)
            {
                error = "Выберите автомобиль из списка.";
                return false;
            }

            var client = ClientName.Trim();
            if (string.IsNullOrEmpty(client))
            {
                error = "Укажите имя клиента.";
                return false;
            }

            if (!StartDate.HasValue || !EndDate.HasValue)
            {
                error = "Укажите даты начала и окончания.";
                return false;
            }

            var start = StartDate.Value.Date;
            var end = EndDate.Value.Date;
            if (end < start)
            {
                error = "Дата окончания не может быть раньше даты начала.";
                return false;
            }

            return true;
        }

        public Booking CreateBooking()
        {
            var car = SelectedCar!;
            return new Booking
            {
                CarName = car.DisplayName,
                ClientName = ClientName.Trim(),
                StartDate = StartDate!.Value.Date,
                EndDate = EndDate!.Value.Date
            };
        }

        public void ApplyTo(Booking booking)
        {
            var car = SelectedCar!;
            booking.CarName = car.DisplayName;
            booking.ClientName = ClientName.Trim();
            booking.StartDate = StartDate!.Value.Date;
            booking.EndDate = EndDate!.Value.Date;
        }

        public void DisposeBindings()
        {
            _bookings.CollectionChanged -= Bookings_CollectionChanged;
        }

        private void Bookings_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(ApplyFilter);
        }

        private void UpdateCarsAvailability()
        {
            foreach (var car in _cars)
            {
                var active = _bookings.Count(b =>
                    b.CarName == car.DisplayName && !ReferenceEquals(b, _editingBooking));
                car.IsFree = active < car.FleetSize;
            }
        }

        private bool FilterCar(object obj)
        {
            if (obj is not Car car) return false;

            var fc = FilterClassIndex switch
            {
                1 => (CarClass?)CarClass.Economy,
                2 => CarClass.Standard,
                3 => CarClass.Premium,
                4 => CarClass.Luxury,
                _ => null
            };
            if (fc.HasValue && car.Class != fc.Value)
                return false;

            if (MinPassengerSeats > 0 && car.PassengerSeats < MinPassengerSeats)
                return false;

            if (OnlyWithAvailableFleet)
            {
                var active = _bookings.Count(b =>
                    b.CarName == car.DisplayName && !ReferenceEquals(b, _editingBooking));
                if (active >= car.FleetSize)
                    return false;
            }

            return true;
        }

        private void ApplyFilter()
        {
            _carsView.Refresh();
            UpdateCarsAvailability();
            if (SelectedCar != null && !_carsView.Cast<object>().Contains(SelectedCar))
                SelectedCar = _carsView.Cast<Car>().FirstOrDefault();
            OnPropertyChanged(nameof(CarsView));
        }

        private void RefreshCostDisplay()
        {
            if (SelectedCar == null || !StartDate.HasValue || !EndDate.HasValue)
                RentalCostDisplay = "—";
            else
            {
                var start = StartDate.Value.Date;
                var end = EndDate.Value.Date;
                if (end < start)
                    RentalCostDisplay = "некорректный период";
                else
                {
                    var days = Math.Max(1, (end - start).Days + 1);
                    var total = days * SelectedCar.PricePerDay;
                    RentalCostDisplay = $"{total:N0} BYN ({days} сут. x {SelectedCar.PricePerDay} BYN)";
                }
            }

            OnPropertyChanged(nameof(RentalCostDisplay));
            OnPropertyChanged(nameof(RentalDays));
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
