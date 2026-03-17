using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Расчёт по двум формулам:");
        Console.WriteLine("z1 = (sin(2a) + sin(5a) - sin(3a)) / (cos(a) + 1 - 2*sin^2(2a))");
        Console.WriteLine("z2 = 2 * sin(a)");
        Console.WriteLine();

        Console.Write("Введите значение a (в радианах): ");
        double a = Convert.ToDouble(Console.ReadLine());
        double numerator = Math.Sin(2 * a) + Math.Sin(5 * a) - Math.Sin(3 * a);
        double denominator = Math.Cos(a) + 1 - 2 * Math.Pow(Math.Sin(2 * a), 2);
        double z1 = numerator / denominator;
        double z2 = 2 * Math.Sin(a);
        Console.WriteLine();
        Console.WriteLine("Результаты вычислений:");
        Console.WriteLine("z1 = " + z1);
        Console.WriteLine("z2 = " + z2);
        Console.WriteLine("\nПроверка: значения должны быть примерно равны.");
    }
}
