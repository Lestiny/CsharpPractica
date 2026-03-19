using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите первую строку: ");
        string s1 = Console.ReadLine();

        Console.Write("Введите вторую строку: ");
        string s2 = Console.ReadLine();

        bool equal = CompareStrings(s1, s2);

        Console.WriteLine(equal ? "Строки равны" : "Строки не равны");
    }

    static bool CompareStrings(string a, string b)
    {
        string s1 = a.Replace(" ", "").ToLower();
        string s2 = b.Replace(" ", "").ToLower();

        return s1 == s2;
    }
}
