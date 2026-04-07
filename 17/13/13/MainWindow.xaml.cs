using System.Windows;

namespace CarRental
{
    public partial class MainWindow : Window
    {
        private readonly CarRentalViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new CarRentalViewModel();
            DataContext = _viewModel;
            Closed += (_, _) => _viewModel.Dispose();
        }
    }
}
