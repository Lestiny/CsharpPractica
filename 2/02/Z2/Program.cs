using System;

class Program
{
    static void Main()
    {
        int n;
        Console.Write("Введите трёхзначное число: ");
        n = int.Parse(Console.ReadLine());

        int a = n / 100;         
        int b = (n / 10) % 10;    
        int c = n % 10;           

        if ((b - a) == (c - b))
        {
            Console.WriteLine("Цифры образуют арифметическую прогрессию");
        }
        else
        {
            Console.WriteLine("Цифры не образуют арифметическую прогрессию");
        }
    }
}
