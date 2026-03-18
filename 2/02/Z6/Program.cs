using System;

class Program
{
    static void Main()
    {
        int n;
        Console.Write("Введите номер пассажира (до 12): ");
        n = int.Parse(Console.ReadLine());

        switch (n)
        {
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                Console.WriteLine("Пассажир грязный.");
                break;

            case 3:
            case 9:
            case 12:
                Console.WriteLine("Пассажир исцарапанный.");
                break;

            case 1:
            case 2:
            case 10:
            case 11:
                Console.WriteLine("Пассажир чистый и целый.");
                break;

            default:
                Console.WriteLine("Такого пассажира нет.");
                break;
        }
    }
}
