using System;

static class IntExtensions
{
    public static int Factorial(this int n)
    {
        if (n < 0)
            throw new ArgumentException("Факториал отрицательного числа не существует");

        if (n == 0 || n == 1)
            return 1;

        return n * Factorial(n - 1);
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите число: ");
        int x = int.Parse(Console.ReadLine());

        Console.WriteLine("Факториал: " + x.Factorial());
    }
}
