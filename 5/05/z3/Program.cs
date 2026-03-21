using System;

class Program
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 2, 4, 2 };

        int count = CountOccurrences(arr, 2);

        Console.WriteLine("Количество вхождений: " + count);
    }

    static int CountOccurrences(int[] array, int value, int index = 0)
    {
        if (index == array.Length)
            return 0;

        int add = array[index] == value ? 1 : 0;

        return add + CountOccurrences(array, value, index + 1);
    }
}
