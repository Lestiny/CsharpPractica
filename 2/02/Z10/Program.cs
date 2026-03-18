using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Двузначные числа, сумма квадратов цифр которых кратна 13:");

        for (int n = 10; n <= 99; n++)
        {
            int a = n / 10;   
            int b = n % 10; 

            int sum = a * a + b * b;

            if (sum % 13 == 0)
            {
                Console.WriteLine(n);
            }
        }
    }
}
