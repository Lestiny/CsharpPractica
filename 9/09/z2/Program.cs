using System;
using MyDequeApp;

class Program
{
    static void Main()
    {
        DequeProcessor<string> d = new DequeProcessor<string>();

        d.AddToStart("первый");
        d.AddToEnd("второй");
        d.AddToEnd("третий");

        Console.WriteLine("Все:");
        d.Show();

        d.RemoveStart();
        d.RemoveEnd();

        Console.WriteLine("После удалений:");
        d.Show();
    }
}
