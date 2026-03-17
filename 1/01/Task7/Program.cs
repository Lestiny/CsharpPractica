using System;
class Program
{
    static void Main()
    {
        double a, b, c, d;
        Console.Write("Введите сторону a: ");
        a = double.Parse(Console.ReadLine());
        Console.Write("Введите сторону b: ");
        b = double.Parse(Console.ReadLine());
        Console.Write("Введите сторону c: ");
        c = double.Parse(Console.ReadLine());
        Console.Write("Введите сторону d: ");
        d = double.Parse(Console.ReadLine());
        double x = ((b - a) * (b - a) + c * c - d * d) / (2 * (b - a));
        double h = Math.Sqrt(c * c - x * x);
        double S = (a + b) / 2 * h;
        Console.WriteLine("Площадь трапеции = " + S);
    }
}
