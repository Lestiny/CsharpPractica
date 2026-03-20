using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        string result = RemoveRepeating(input);

        Console.WriteLine("Результат: " + result);
    }

    static string RemoveRepeating(string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;

        string result = "" + s[0];

        for (int i = 1; i < s.Length; i++)
            if (s[i] != s[i - 1])
                result += s[i];

        return result;
    }
}
