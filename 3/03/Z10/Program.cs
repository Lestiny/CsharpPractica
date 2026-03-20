using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите текст: ");
        string input = Console.ReadLine();

        string[] sentences = SplitIntoSentences(input);

        Console.WriteLine("\nПредложения:");
        foreach (string s in sentences)
            Console.WriteLine(s);
    }

    static string[] SplitIntoSentences(string text)
    {
        char[] separators = { '.', '!', '?' };

        string[] parts = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < parts.Length; i++)
            parts[i] = parts[i].Trim();

        return parts;
    }
}
