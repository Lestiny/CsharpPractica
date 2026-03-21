using System;

class Program
{
    static void NextWeekday(ref int D, ref int M, ref int Y)
    {
        DateTime date = new DateTime(Y, M, D);

        date = date.AddDays(1);

 
        while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            date = date.AddDays(1);
        }

        D = date.Day;
        M = date.Month;
        Y = date.Year;
    }

    static void Main()
    {
        int d1 = 22, m1 = 3, y1 = 2026;
        int d2 = 23, m2 = 3, y2 = 2026;
        int d3 = 24, m3 = 3, y3 = 2026;

        NextWeekday(ref d1, ref m1, ref y1);
        NextWeekday(ref d2, ref m2, ref y2);
        NextWeekday(ref d3, ref m3, ref y3);

        Console.WriteLine($"Следующий рабочий день: {d1}.{m1}.{y1}");
        Console.WriteLine($"Следующий рабочий день: {d2}.{m2}.{y2}");
        Console.WriteLine($"Следующий рабочий день: {d3}.{m3}.{y3}");
    }
}