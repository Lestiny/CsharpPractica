using System;

class Program
{
    static void Main()
    {
        int A, B;

        Console.Write("Введите A: ");
        A = int.Parse(Console.ReadLine());

        Console.Write("Введите B: ");
        B = int.Parse(Console.ReadLine());

        Console.WriteLine("\nwhile");
        int i = A;
        while (i <= B)
        {
            if (i % 3 == 0)
                Console.Write(i + " ");
            i++;
        }

        Console.WriteLine("\n\ndo while");
        i = A;
        do
        {
            if (i % 3 == 0)
                Console.Write(i + " ");
            i++;
        }
        while (i <= B);

        Console.WriteLine("\n\nfor");
        for (i = A; i <= B; i++)
        {
            if (i % 3 == 0)
                Console.Write(i + " ");
        }

        Console.WriteLine();
    }
}
