using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите вес в фунтах:");
        string input = Console.ReadLine();
        double pounds = Convert.ToDouble(input);
        double kilograms = pounds * 0.4095;
        Console.WriteLine(pounds + " фунтов = " + kilograms.ToString("0.00") + " кг.");
    }
}
