using System;

class Program
{
    static void Main()
    {
        int[,] house = new int[12, 4];
        Random rnd = new Random();

        for (int i = 0; i < 12; i++)
            for (int j = 0; j < 4; j++)
                house[i, j] = rnd.Next(1,6);

        Console.WriteLine("Количество жильцов(12 этажей по 4 квартиры):\n");

        for (int i = 0; i < 12; i++)
        {
            Console.Write($"Этаж {i + 1,2}: ");
            for (int j = 0; j < 4; j++)
                Console.Write(house[i, j] + " ");
            Console.WriteLine();
        }

        int maxFamily = 0;

        for (int i = 2; i <= 3; i++)
            for (int j = 0; j < 4; j++)
                if (house[i, j] > maxFamily)
                    maxFamily = house[i, j];

        Console.WriteLine($"\nСамая большая семья на 3-4 этажах: {maxFamily}");
    }
}
