using System;

namespace TerminalHistoryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TerminalHistory h = new TerminalHistory();

            h.Add("ls");
            h.Add("cd folder");
            h.Add("mkdir test");

            Console.WriteLine("Все команды:");
            h.Print();

            Console.WriteLine("Удаляем последнюю:");
            var r = h.Remove();
            if (r != null) Console.WriteLine(r.CommandText);

            Console.WriteLine("Поиск 'cd':");
            var f = h.Find("cd");
            foreach (var c in f)
                Console.WriteLine(c.CommandText);
        }
    }
}
