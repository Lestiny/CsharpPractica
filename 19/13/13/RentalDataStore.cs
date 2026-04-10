using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace CarRental
{
    public class AuthSessionState
    {
        public string Username { get; set; } = "";
    }

    public class RentalState
    {
        public List<CarModel> Cars { get; set; } = new();
        public List<RentalModel> Rentals { get; set; } = new();
    }

    public class RentalDataStore
    {
        private readonly string _baseDirectory;
        private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        public RentalDataStore(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
            Directory.CreateDirectory(_baseDirectory);
        }

        public string RentalJsonPath => Path.Combine(_baseDirectory, "rental.json");
        public string RentalXmlPath => Path.Combine(_baseDirectory, "rental.xml");
        public string UsersJsonPath => Path.Combine(_baseDirectory, "users.json");
        public string AuthSessionJsonPath => Path.Combine(_baseDirectory, "auth-session.json");

        public RentalState LoadRentalState()
        {
            if (!File.Exists(RentalJsonPath))
            {
                var seed = CreateDefaultState();
                SaveRentalState(seed);
                return seed;
            }

            var content = File.ReadAllText(RentalJsonPath);
            var state = JsonSerializer.Deserialize<RentalState>(content, _jsonOptions) ?? new RentalState();
            var defaults = CreateDefaultState();

            var legacyF1Car = state.Cars.FirstOrDefault(c =>
                string.Equals(c.Make, "Red Bull", StringComparison.OrdinalIgnoreCase) &&
                c.Model.Contains("RB20", StringComparison.OrdinalIgnoreCase));
            if (legacyF1Car != null)
            {
                var oldDisplayName = legacyF1Car.DisplayName;
                legacyF1Car.Make = "Mercedes-AMG";
                legacyF1Car.Model = "F1 W16 (2026)";
                legacyF1Car.PricePerDay = 6500;
                var newDisplayName = legacyF1Car.DisplayName;

                foreach (var rental in state.Rentals.Where(r => string.Equals(r.CarName, oldDisplayName, StringComparison.OrdinalIgnoreCase)))
                    rental.CarName = newDisplayName;
            }

            var oldMercedesF1 = state.Cars.FirstOrDefault(c =>
                string.Equals(c.Make, "Mercedes-AMG", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.Model, "F1 W15", StringComparison.OrdinalIgnoreCase));
            if (oldMercedesF1 != null)
            {
                var oldDisplayName = oldMercedesF1.DisplayName;
                oldMercedesF1.Model = "F1 W16 (2026)";
                oldMercedesF1.PricePerDay = 6500;
                var newDisplayName = oldMercedesF1.DisplayName;

                foreach (var rental in state.Rentals.Where(r => string.Equals(r.CarName, oldDisplayName, StringComparison.OrdinalIgnoreCase)))
                    rental.CarName = newDisplayName;
            }

            var mercedesF1 = state.Cars.FirstOrDefault(c =>
                string.Equals(c.Make, "Mercedes-AMG", StringComparison.OrdinalIgnoreCase) &&
                c.Model.StartsWith("F1", StringComparison.OrdinalIgnoreCase));
            if (mercedesF1 != null)
            {
                var oldDisplayName = mercedesF1.DisplayName;
                mercedesF1.Model = "F1 W16 (2026)";
                mercedesF1.PricePerDay = 6500;
                var newDisplayName = mercedesF1.DisplayName;

                if (!string.Equals(oldDisplayName, newDisplayName, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var rental in state.Rentals.Where(r => string.Equals(r.CarName, oldDisplayName, StringComparison.OrdinalIgnoreCase)))
                        rental.CarName = newDisplayName;
                }
            }

            if (state.Cars.Count == 0)
            {
                state.Cars = defaults.Cars;
            }
                else
                {
                    var existing = new HashSet<string>(state.Cars.Select(c => c.DisplayName), StringComparer.OrdinalIgnoreCase);
                foreach (var car in defaults.Cars)
                {
                    if (!existing.Contains(car.DisplayName))
                        state.Cars.Add(car);
                }
            }

            return state;
        }

        public void SaveRentalState(RentalState state)
        {
            var json = JsonSerializer.Serialize(state, _jsonOptions);
            File.WriteAllText(RentalJsonPath, json);

            var serializer = new XmlSerializer(typeof(RentalState));
            using var fs = File.Create(RentalXmlPath);
            serializer.Serialize(fs, state);
        }

        public List<UserAccount> LoadUsers()
        {
            if (!File.Exists(UsersJsonPath))
            {
                var users = new List<UserAccount>();
                SaveUsers(users);
                return users;
            }

            var json = File.ReadAllText(UsersJsonPath);
            return JsonSerializer.Deserialize<List<UserAccount>>(json, _jsonOptions) ?? new List<UserAccount>();
        }

        public void SaveUsers(List<UserAccount> users)
        {
            var json = JsonSerializer.Serialize(users, _jsonOptions);
            File.WriteAllText(UsersJsonPath, json);
        }

        public string? LoadLastUsername()
        {
            if (!File.Exists(AuthSessionJsonPath))
                return null;

            var json = File.ReadAllText(AuthSessionJsonPath);
            var session = JsonSerializer.Deserialize<AuthSessionState>(json, _jsonOptions);
            if (session == null || string.IsNullOrWhiteSpace(session.Username))
                return null;

            return session.Username;
        }

        public void SaveLastUsername(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                if (File.Exists(AuthSessionJsonPath))
                    File.Delete(AuthSessionJsonPath);
                return;
            }

            var session = new AuthSessionState { Username = username.Trim() };
            var json = JsonSerializer.Serialize(session, _jsonOptions);
            File.WriteAllText(AuthSessionJsonPath, json);
        }

        private static RentalState CreateDefaultState()
        {
            return new RentalState
            {
                Cars = new List<CarModel>
                {
                    new() { Make = "Lada", Model = "Granta", PricePerDay = 70, FleetSize = 3 },
                    new() { Make = "Hyundai", Model = "Solaris", PricePerDay = 95, FleetSize = 2 },
                    new() { Make = "Audi", Model = "A4 B5", PricePerDay = 120, FleetSize = 2 },
                    new() { Make = "Toyota", Model = "Camry", PricePerDay = 170, FleetSize = 1 },
                    new() { Make = "BMW", Model = "M5", PricePerDay = 420, FleetSize = 1 },
                    new() { Make = "Kia", Model = "Rio", PricePerDay = 88, FleetSize = 2 },
                    new() { Make = "Skoda", Model = "Octavia", PricePerDay = 110, FleetSize = 2 },
                    new() { Make = "Mercedes", Model = "E200", PricePerDay = 260, FleetSize = 1 },
                    new() { Make = "Volkswagen", Model = "Caravelle", PricePerDay = 220, FleetSize = 2 },
                    new() { Make = "Mercedes-AMG", Model = "F1 W16 (2026)", PricePerDay = 6500, FleetSize = 1 }
                }
            };
        }
    }
}
