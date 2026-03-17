using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите четырёхзначное число: ");
        int num = Convert.ToInt32(Console.ReadLine());
        int a = num / 1000;
        int b = (num / 100) % 10;
        int c = (num / 10) % 10;
        int d = num % 10;
        int result = c * 1000 + d * 100 + a * 10 + b;
        Console.WriteLine("Полученное число " + result);
    }
}
