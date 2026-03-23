using System;

namespace GameCharacters
{
    class GameCharacter
    {
        public string Name;

        public GameCharacter(string name)
        {
            Name = name;
        }
    }

    interface IMeleeFighter
    {
        void MeleeAttack();
    }

    interface IRangedFighter
    {
        void RangedAttack();
    }

    class Knight : GameCharacter, IMeleeFighter
    {
        public Knight(string name) : base(name) { }

        public void MeleeAttack()
        {
            Console.WriteLine(Name + " использует нож");
        }
    }

    class Archer : GameCharacter, IRangedFighter
    {
        public Archer(string name) : base(name) { }

        public void RangedAttack()
        {
            Console.WriteLine(Name + " стреляет из лука");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество персонажей: ");
            int n = int.Parse(Console.ReadLine());

            GameCharacter[] characters = new GameCharacter[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("\nИмя для игрока " + (i + 1));
                Console.Write("Введите имя: ");
                string name = Console.ReadLine();

                Console.Write("Тип (1 - мечник, 2 - лучник): ");
                int type = int.Parse(Console.ReadLine());

                if (type == 1)
                {
                    characters[i] = new Knight(name);
                }
                else
                {
                    characters[i] = new Archer(name);
                }
            }

            Console.WriteLine("\nЛучники:");

            foreach (GameCharacter character in characters)
            {
                if (character is IRangedFighter)
                {
                    Console.WriteLine(character.Name);
                }
            }

            Console.ReadKey();
        }
    }
}