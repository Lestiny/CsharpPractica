using System;

class Program
{
    static void Main()
    {
        double A1 = 1, B1 = 2, C1 = 3;
        double A2 = 4.5, B2 = 5.5, C2 = 6.5;

        Console.WriteLine("До:");
        Console.WriteLine(A1 + " " + B1 + " " + C1);
        Console.WriteLine(A2 + " " + B2 + " " + C2);

        ShiftRight3(ref A1, ref B1, ref C1);
        ShiftRight3(ref A2, ref B2, ref C2);

        Console.WriteLine("\nПосле:");
        Console.WriteLine(A1 + " " + B1 + " " + C1);
        Console.WriteLine(A2 + " " + B2 + " " + C2);
    }

    static void ShiftRight3(ref double A, ref double B, ref double C)
    {
        double t = C;
        C = B;
        B = A;
        A = t;
    }
}
