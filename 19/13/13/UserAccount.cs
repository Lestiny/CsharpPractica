namespace CarRental
{
    public enum UserRole
    {
        Client = 0,
        Manager = 1
    }

    public class UserAccount
    {
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public UserRole Role { get; set; } = UserRole.Client;
    }
}
