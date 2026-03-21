using System;

class Program
{
    static void Main()
    {
        Console.WriteLine(CalculateCircumference(5).ToString("F2"));
        Console.WriteLine(CalculateCircumference(4, 6));   
    }

    static double CalculateCircumference(double radius)
    {
        return 2 * Math.PI * radius;
    }

    static double CalculateCircumference(double length, double width)
    {
        return 2 * (length + width);
    }
}
