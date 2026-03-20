using System;

class Program
{
    static void Main()
    {
        int[][] a = new int[][]
        {
            new int[] {1, 3, 5, 7},
            new int[] {2, 4, 6, 8, 10},
            new int[] {11, 13, 15},
            new int[] {16, 18, 20, 22}
        };

        Console.WriteLine("Массив:");

        for (int i = 0; i < a.Length; i++)
        {
            foreach (int x in a[i])
                Console.Write(x + " ");
            Console.WriteLine();
        }

        Console.Write("\nВведите число: ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < a.Length; i++)
        {
            int left = 0, right = a[i].Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (a[i][mid] == n)
                {
                    Console.WriteLine($"Найдено в строке {i}, индекс {mid}");
                    return;
                }

                if (a[i][mid] < n)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
        }

        Console.WriteLine("Число не найдено");
    }
}