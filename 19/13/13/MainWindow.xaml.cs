using System.Windows;

namespace CarRental
{
    public partial class MainWindow : Window
    {
        private readonly CarRentalViewModel _viewModel;

        public MainWindow(UserAccount user, CarRentalDbContext dbContext)
        {
            InitializeComponent();
            _viewModel = new CarRentalViewModel(user, dbContext);
            DataContext = _viewModel;
            Closed += (_, _) =>
            {
                _viewModel.Dispose();
            };
        }

        private void LogoutAndRelogin_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Logout();
            if (Application.Current is App app)
                app.LogoutAndSwitchUser(this);
        }
    }
}
