using System;

class Program
{
    static void Main()
    {
        double candyPrice, cookiePrice, applePrice;
        double x, y, z;

        Console.Write("Цена 1кг конфет: ");
        candyPrice = double.Parse(Console.ReadLine());

        Console.Write("Цена 1кг печенья: ");
        cookiePrice = double.Parse(Console.ReadLine());

        Console.Write("Цена 1кг яблок: ");
        applePrice = double.Parse(Console.ReadLine());

        Console.Write("Сколько кг конфет купили: ");
        x = double.Parse(Console.ReadLine());

        Console.Write("Сколько кг печенья купили: ");
        y = double.Parse(Console.ReadLine());

        Console.Write("Сколько кг яблок купили: ");
        z = double.Parse(Console.ReadLine());

        double total = candyPrice * x + cookiePrice * y + applePrice * z;

        Console.WriteLine("Общая стоимость покупки = " + total);
    }
}
