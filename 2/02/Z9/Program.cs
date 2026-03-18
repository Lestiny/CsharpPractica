using System;

class Program
{
    static void Main()
    {
        double A = Math.PI / 6;      
        double B = 2 * Math.PI / 3;  
        int M = 10;

        double H = (B - A) / M;      

        Console.WriteLine("Таблица значений функции sin(x^2):");
        Console.WriteLine($"A = {A}, B = {B}, M = {M}, H = {H}\n");

        double x = A;

        for (int i = 0; i <= M; i++)
        {
            double y = Math.Sin(x * x);
            Console.WriteLine("x = " + x.ToString("F2"));
            Console.WriteLine("y = " + y.ToString("F2"));
            x += H;
        }
    }
}
