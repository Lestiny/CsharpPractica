using System;

class Program
{
    static void Main()
    {
        double x;
        Console.Write("Введите x: ");
        x = double.Parse(Console.ReadLine());

        double y;

        if (x >= 1 && x <= 5)
        {
            y = Math.Log(x) + Math.Pow(Math.Cos(x * x), 2);
            Console.WriteLine("y = " + y);
        }
        else if (Math.Abs(x - Math.PI) < 0.000001)
        {
            y = Math.Pow(Math.Sin(x), 2);
            Console.WriteLine("y = " + y);
        }
        else
        {
            Console.WriteLine("Функция не определена");
        }
    }
}
