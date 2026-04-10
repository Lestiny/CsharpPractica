using System.Windows;

namespace CarRental
{
    public partial class LoginWindow : Window
    {
        private readonly RentalDataStore _store;
        private readonly AuthService _authService;

        public UserAccount? LoggedInUser { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();

            var appData = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Data");
            _store = new RentalDataStore(appData);
            _authService = new AuthService(_store);

            var last = _store.LoadLastUsername();
            if (!string.IsNullOrWhiteSpace(last))
                UsernameBox.Text = last;

            HintText.Text = "";
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var user = _authService.Login(UsernameBox.Text, PasswordBox.Password);
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль.", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            LoggedInUser = user;
            _store.SaveLastUsername(user.Username);
            DialogResult = true;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var role = GetSelectedRole();
            if (_authService.Register(UsernameBox.Text, PasswordBox.Password, role, out var message))
            {
                var user = _authService.Login(UsernameBox.Text, PasswordBox.Password);
                if (user == null)
                {
                    MessageBox.Show(message, "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                    PasswordBox.Password = "";
                    return;
                }

                LoggedInUser = user;
                _store.SaveLastUsername(user.Username);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show(message, "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private UserRole GetSelectedRole()
        {
            if (RoleBox.SelectedItem is FrameworkElement fe && fe.Tag is UserRole role)
                return role;
            return UserRole.Client;
        }
    }
}
