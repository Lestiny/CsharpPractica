using System;

class Program
{
    static void DetermineSign(in double number, out string result)
    {
        if (number > 0)
            result = "Положительное";
        else if (number < 0)
            result = "Отрицательное";
        else
            result = "Ноль";
    }

    static void DetermineSign(in int number, out string result)
    {
        if (number > 0)
            result = "Положительное";
        else if (number < 0)
            result = "Отрицательное";
        else
            result = "Ноль";
    }

    static void Main()
    {
        string signResult;

        int a = -5;
        int b = 0;
        double c = 10.5;

        DetermineSign(in a, out signResult);
        Console.WriteLine(signResult);

        DetermineSign(in b, out signResult);
        Console.WriteLine(signResult);

        DetermineSign(in c, out signResult);
        Console.WriteLine(signResult);
    }
}