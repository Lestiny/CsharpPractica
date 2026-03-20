using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        string result = RemoveNonAlphanumeric(input);

        Console.WriteLine("Результат: " + result);
    }

    static string RemoveNonAlphanumeric(string s)
    {
        string result = "";

        foreach (char c in s)
            if (char.IsLetterOrDigit(c))
                result += c;

        return result;
    }
}
