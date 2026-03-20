using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество элементов: ");
        int p = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите элементы:");
        string[] parts = Console.ReadLine().Split();

        int[] arr = new int[p];
        for (int i = 0; i < p; i++)
            arr[i] = int.Parse(parts[i]);

        bool evenAfterOdd = false;

        for (int i = 1; i < p; i++)
        {
            if (arr[i - 1] % 2 != 0 && arr[i] % 2 == 0)
            {
                evenAfterOdd = true;
                break;
            }
        }

        if (!evenAfterOdd)
        {
            for (int i = p - 1; i >= 0; i--)
                if (arr[i] < 0)
                    Console.Write(arr[i] + " ");
        }
        else
        {
            for (int i = p - 1; i >= 0; i--)
                if (arr[i] > 0)
                    Console.Write(arr[i] + " ");
        }
    }
}
