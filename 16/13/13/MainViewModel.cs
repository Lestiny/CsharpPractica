using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CarRental
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Booking? _selectedBooking;

        public ObservableCollection<Car> Cars { get; } = new();
        public ObservableCollection<Booking> Bookings { get; } = new();

        public Booking? SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                if (_selectedBooking != value)
                {
                    _selectedBooking = value;
                    OnPropertyChanged();
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public ICommand BookCarCommand { get; }
        public ICommand EditBookingCommand { get; }
        public ICommand CancelBookingCommand { get; }

        public MainViewModel()
        {
            Cars.Add(new Car
            {
                Make = "Lada",
                Model = "Granta",
                PricePerDay = 70,
                Class = CarClass.Economy,
                PassengerSeats = 5,
                FleetSize = 3
            });
            Cars.Add(new Car
            {
                Make = "Hyundai",
                Model = "Solaris",
                PricePerDay = 95,
                Class = CarClass.Standard,
                PassengerSeats = 5,
                FleetSize = 2
            });
            Cars.Add(new Car
            {
                Make = "Audi",
                Model = "A4 B5",
                PricePerDay = 120,
                Class = CarClass.Standard,
                PassengerSeats = 5,
                FleetSize = 2
            });
            Cars.Add(new Car
            {
                Make = "Toyota",
                Model = "Camry",
                PricePerDay = 170,
                Class = CarClass.Premium,
                PassengerSeats = 5,
                FleetSize = 1
            });
            Cars.Add(new Car
            {
                Make = "BMW",
                Model = "M5",
                PricePerDay = 420,
                Class = CarClass.Luxury,
                PassengerSeats = 5,
                FleetSize = 1
            });
            Cars.Add(new Car
            {
                Make = "Volkswagen",
                Model = "Caravelle",
                PricePerDay = 220,
                Class = CarClass.Standard,
                PassengerSeats = 8,
                FleetSize = 2
            });

            BookCarCommand = new RelayCommand(_ => OpenBookCar(), _ => true);
            EditBookingCommand = new RelayCommand(_ => OpenEditBooking(), _ => true);
            CancelBookingCommand = new RelayCommand(_ => CancelBooking(), _ => true);
        }

        private void OpenBookCar()
        {
            var dlg = new BookingWindow(Cars, Bookings) { Owner = Application.Current.MainWindow };
            if (dlg.ShowDialog() == true && dlg.ResultBooking != null)
                Bookings.Add(dlg.ResultBooking);
        }

        private void OpenEditBooking()
        {
            if (SelectedBooking == null)
            {
                MessageBox.Show("Сначала выберите бронь в списке.", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var dlg = new EditBookingWindow(Cars, Bookings, SelectedBooking) { Owner = Application.Current.MainWindow };
            dlg.ShowDialog();
        }

        private void CancelBooking()
        {
            if (SelectedBooking == null)
            {
                MessageBox.Show("Сначала выберите бронь в списке.", "Отмена бронирования", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var b = SelectedBooking;
            var msg = $"Отменить бронь «{b.CarName}» для клиента «{b.ClientName}»?";
            var r = MessageBox.Show(msg, "Отмена бронирования", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r != MessageBoxResult.Yes) return;
            Bookings.Remove(b);
            SelectedBooking = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
