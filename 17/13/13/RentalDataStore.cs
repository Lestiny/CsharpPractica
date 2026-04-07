using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace CarRental
{
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

            if (state.Cars.Count == 0)
                state.Cars = CreateDefaultState().Cars;

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
                    new() { Make = "BMW", Model = "M5", PricePerDay = 420, FleetSize = 1 }
                }
            };
        }
    }
}
