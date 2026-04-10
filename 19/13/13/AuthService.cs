using System.Security.Cryptography;
using System.Text;

namespace CarRental
{
    public class AuthService
    {
        private readonly RentalDataStore _store;

        public AuthService(RentalDataStore store)
        {
            _store = store;
        }

        public bool Register(string username, string password, UserRole role, out string message)
        {
            var login = username.Trim();
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                message = "Введите логин и пароль.";
                return false;
            }

            var users = _store.LoadUsers();
            if (users.Any(u => string.Equals(u.Username, login, StringComparison.OrdinalIgnoreCase)))
            {
                message = "Пользователь с таким логином уже существует.";
                return false;
            }

            users.Add(new UserAccount
            {
                Username = login,
                PasswordHash = Hash(password),
                Role = role
            });
            _store.SaveUsers(users);
            message = "Регистрация выполнена.";
            return true;
        }

        public UserAccount? Login(string username, string password)
        {
            var login = username.Trim();
            var hash = Hash(password);
            var users = _store.LoadUsers();
            return users.FirstOrDefault(u =>
                string.Equals(u.Username, login, StringComparison.OrdinalIgnoreCase)
                && u.PasswordHash == hash);
        }

        private static string Hash(string source)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(source));
            return Convert.ToHexString(bytes);
        }
    }
}
