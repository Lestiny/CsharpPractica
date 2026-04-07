using System.Collections.ObjectModel;
using System.Windows;

namespace CarRental
{
    public partial class EditBookingWindow : Window
    {
        private readonly Booking _booking;

        public EditBookingWindow(ObservableCollection<Car> cars, ObservableCollection<Booking> bookings, Booking booking)
        {
            InitializeComponent();
            _booking = booking;
            var vm = new BookingDialogViewModel(cars, bookings, booking);
            DataContext = vm;
            Closed += (_, _) => vm.DisposeBindings();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not BookingDialogViewModel vm) return;
            if (!vm.Validate(out var err))
            {
                MessageBox.Show(err, "Редактирование", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            vm.ApplyTo(_booking);
            DialogResult = true;
        }
    }
}
