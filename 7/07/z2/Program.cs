using System;

delegate void KeyEventHandler(ConsoleKey key);

class Program
{
    static void Main()
    {
        Console.WriteLine("Нажмите Enter или Esc");
        var key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.Enter)
            HandleKeyPress(key, OnKeyEnter);
        else if (key == ConsoleKey.Escape)
            HandleKeyPress(key, OnKeyEscape);
        else
            Console.WriteLine("Другая клавиша");
    }

    static void HandleKeyPress(ConsoleKey key, KeyEventHandler handler)
    {
        handler(key);
    }

    static void OnKeyEnter(ConsoleKey key)
    {
        Console.WriteLine("Нажат Enter");
    }

    static void OnKeyEscape(ConsoleKey key)
    {
        Console.WriteLine("Нажат Escape");
    }
}
