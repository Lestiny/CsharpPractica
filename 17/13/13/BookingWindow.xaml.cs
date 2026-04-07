using System.Collections.ObjectModel;
using System.Windows;

namespace CarRental
{
    public partial class BookingWindow : Window
    {
        public Booking? ResultBooking { get; private set; }

        public BookingWindow(ObservableCollection<Car> cars, ObservableCollection<Booking> bookings)
        {
            InitializeComponent();
            var vm = new BookingDialogViewModel(cars, bookings);
            DataContext = vm;
            Closed += (_, _) => vm.DisposeBindings();
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not BookingDialogViewModel vm) return;
            if (!vm.Validate(out var err))
            {
                MessageBox.Show(err, "Бронирование", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ResultBooking = vm.CreateBooking();
            DialogResult = true;
        }
    }
}
