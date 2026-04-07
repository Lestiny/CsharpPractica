using System.Windows;

namespace CarRental
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CarRentalViewModel();
        }
    }
}
