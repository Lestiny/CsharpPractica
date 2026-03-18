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

        int sum = 0;

        for (int i = A; i <= B; i++)
        {
            sum += i * i; 
        }

        Console.WriteLine("Сумма квадратов = " + sum);
    }
}
