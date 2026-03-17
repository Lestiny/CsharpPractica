using System;
class Program
{
    static void Main()
    {
        double x = -1;
        Console.WriteLine("х =" + x);
        double inside = Math.Exp(x) + 1 + Math.Abs(x);
        double sqrtVal = Math.Sqrt(inside);
        double arcctg = Math.PI / 2 - Math.Atan(sqrtVal);
        double y = 7 * Math.Pow(arcctg, 2);
        Console.WriteLine("y = " + y);
    }
}
