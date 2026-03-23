using System;

namespace UsersSystem
{
    abstract class User
    {
        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
        }

        public abstract string GetPermissions();
    }

    class Admin : User
    {
        public Admin(string name) : base(name)
        {
        }

        public override string GetPermissions()
        {
            return "Полный доступ";
        }
    }

    class Moderator : User
    {
        public Moderator(string name) : base(name)
        {
        }

        public override string GetPermissions()
        {
            return "Изменение";
        }
    }

    class Guest : User
    {
        public Guest(string name) : base(name)
        {
        }

        public override string GetPermissions()
        {
            return "Только чтение";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            User[] users = new User[3];

            users[0] = new Admin("Паша");
            users[1] = new Moderator("Саша");
            users[2] = new Guest("Маша");

            Console.WriteLine("Права пользователей:\n");

            foreach (User user in users)
            {
                Console.WriteLine("Пользователь: " + user.Name);
                Console.WriteLine("Права: " + user.GetPermissions());
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}