using System;

class Program
{
    static void Main()
    {
        int N;
        Console.Write("Введите N(1 <= n <= 10): ");
        N = int.Parse(Console.ReadLine());

        int sum = 0;
        int odd = 1;

        for (int i = 1; i <= N; i++)
        {
            sum += odd;      
            Console.WriteLine(sum); 
            odd += 2;       
        }
    }
}
