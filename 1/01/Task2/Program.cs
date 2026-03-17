using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите трёхзначное число: ");
        int num = Convert.ToInt32(Console.ReadLine());
        int second = (num / 10) % 10;
        int last = num % 10;
        int result = second * last;
        Console.WriteLine("Произведение второй и последней цифры = " + result);
    }
}
