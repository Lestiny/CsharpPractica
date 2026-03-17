using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите первое число: ");
        int a = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите второе число: ");
        int b = Convert.ToInt32(Console.ReadLine());
        int sum = a + b;
        int diff = a - b;
        int mult = a * b;
        Console.WriteLine();
        Console.WriteLine("a=" + a);
        Console.WriteLine("b=" + b);
        Console.WriteLine("a+b=" + sum);
        Console.WriteLine("a-b=" + diff);
        Console.WriteLine("a*b=" + mult);
    }
}
