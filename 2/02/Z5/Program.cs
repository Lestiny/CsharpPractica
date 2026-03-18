using System;

class Program
{
    static void Main()
    {
        int n;
        Console.Write("Введите трёхзначное число: ");
        n = int.Parse(Console.ReadLine());

        int first = n / 100; 
        int last = n % 10;   

        if (first > last)
        {
            Console.WriteLine("Первая цифра больше");
        }
        else if (last > first)
        {
            Console.WriteLine("Последняя цифра больше");
        }
        else
        {
            Console.WriteLine("Цифры равны");
        }
    }
}
