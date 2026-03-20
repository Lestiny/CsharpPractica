using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите числа массива через пробел:");
        string[] parts = Console.ReadLine().Split();
        int n = parts.Length;
        int[] arr = new int[n];

        for (int i = 0; i < n; i++)
            arr[i] = int.Parse(parts[i]);

        Console.Write("Введите M: ");
        int M = int.Parse(Console.ReadLine());

        int sum = 0;
        int count = 0;

        for (int i = 0; i < n; i++)
            if (arr[i] < M)
            {
                sum += arr[i];
                count++;
            }

        if (count > 0)
            Console.WriteLine("Среднее арифметическое: " + (sum / (double)count));
        else
            Console.WriteLine("Нет чисел меньше M");

        Console.Write("\nВведите количество строк матрицы: ");
        int r = int.Parse(Console.ReadLine());

        Console.Write("Введите количество столбцов матрицы: ");
        int c = int.Parse(Console.ReadLine());

        int[,] a = new int[r, c];

        Console.WriteLine("Введите элементы матрицы построчно через пробел:");
        for (int i = 0; i < r; i++)
        {
            string[] row = Console.ReadLine().Split();
            for (int j = 0; j < c; j++)
                a[i, j] = int.Parse(row[j]);
        }

        Console.WriteLine("\nСумма положительных элементов каждого столбца:");
        for (int j = 0; j < c; j++)
        {
            int colSum = 0;

            for (int i = 0; i < r; i++)
                if (a[i, j] > 0)
                    colSum += a[i, j];

            Console.WriteLine(colSum);
        }
    }
}
