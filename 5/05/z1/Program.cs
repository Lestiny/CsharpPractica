using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите год: ");
        int year = int.Parse(Console.ReadLine());

        if (IsLeapYear(year))
            Console.WriteLine("Високосный год");
        else
            Console.WriteLine("Обычный год");
    }

    public static bool IsLeapYear(int year)
    {
        return (year % 400 == 0) ||
               (year % 4 == 0 && year % 100 != 0);
    }
}
