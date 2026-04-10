using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace CarRental
{
    public partial class App : Application
    {
        private MainWindow? _currentMain;
        private CarRentalDbContext? _dbContext;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var appData = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Data");
            var store = new RentalDataStore(appData);

            var lastUsername = store.LoadLastUsername();
            if (!string.IsNullOrWhiteSpace(lastUsername))
            {
                var user = store.LoadUsers()
                    .FirstOrDefault(u => string.Equals(u.Username, lastUsername, StringComparison.OrdinalIgnoreCase));
                if (user != null)
                {
                    ShowMain(user);
                    return;
                }

                store.SaveLastUsername(null);
            }

            ShowLogin();
        }

        public bool ShowLogin()
        {
            var win = new LoginWindow { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            var ok = win.ShowDialog() == true && win.LoggedInUser != null;
            if (!ok)
            {
                return false;
            }

            ShowMain(win.LoggedInUser!);
            return true;
        }

        public void ShowMain(UserAccount user)
        {
            var appData = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Data");
            var dbPath = System.IO.Path.Combine(appData, "car-rental.db");

            _dbContext?.Dispose();
            _dbContext = new CarRentalDbContext(
                new DbContextOptionsBuilder<CarRentalDbContext>()
                    .UseSqlite($"Data Source={dbPath}")
                    .Options);

            _dbContext.Database.EnsureCreated();
            SeedDatabaseIfEmpty(_dbContext);

            var main = new MainWindow(user, _dbContext);
            _currentMain?.Close();
            _currentMain = main;
            MainWindow = main;
            main.Show();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        public void LogoutAndSwitchUser(MainWindow current)
        {
            current.Hide();
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var ok = ShowLogin();
            if (!ok)
            {
                Shutdown();
                return;
            }

        }

        private static void SeedDatabaseIfEmpty(CarRentalDbContext context)
        {
            if (context.Cars.Any()) return;

            context.Cars.AddRange(
                new CarEntity { Make = "Lada", Model = "Granta", PricePerDay = 70, FleetSize = 3 },
                new CarEntity { Make = "Hyundai", Model = "Solaris", PricePerDay = 95, FleetSize = 2 },
                new CarEntity { Make = "Audi", Model = "A4 B5", PricePerDay = 120, FleetSize = 2 },
                new CarEntity { Make = "Toyota", Model = "Camry", PricePerDay = 170, FleetSize = 1 },
                new CarEntity { Make = "BMW", Model = "M5", PricePerDay = 420, FleetSize = 1 },
                new CarEntity { Make = "Kia", Model = "Rio", PricePerDay = 88, FleetSize = 2 },
                new CarEntity { Make = "Skoda", Model = "Octavia", PricePerDay = 110, FleetSize = 2 },
                new CarEntity { Make = "Mercedes", Model = "E200", PricePerDay = 260, FleetSize = 1 },
                new CarEntity { Make = "Volkswagen", Model = "Caravelle", PricePerDay = 220, FleetSize = 2 },
                new CarEntity { Make = "Mercedes-AMG", Model = "F1 W16 (2026)", PricePerDay = 6500, FleetSize = 1 }
            );
            context.SaveChanges();
        }
    }
}
