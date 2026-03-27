using System;

namespace MyDequeApp
{
    public class MyDeque<T>
    {
        private T[] items = new T[10];
        private int count = 0;

        public void AddFirst(T item)
        {
            if (count == items.Length)
                Array.Resize(ref items, items.Length + 10);

            for (int i = count; i > 0; i--)
                items[i] = items[i - 1];

            items[0] = item;
            count++;
        }

        public void AddLast(T item)
        {
            if (count == items.Length)
                Array.Resize(ref items, items.Length + 10);

            items[count] = item;
            count++;
        }

        public T RemoveFirst()
        {
            if (count == 0) return default;

            T val = items[0];

            for (int i = 0; i < count - 1; i++)
                items[i] = items[i + 1];

            count--;
            return val;
        }

        public T RemoveLast()
        {
            if (count == 0) return default;

            T val = items[count - 1];
            count--;
            return val;
        }

        public void Print()
        {
            for (int i = 0; i < count; i++)
                Console.WriteLine(items[i]);
        }
    }
}
