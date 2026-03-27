using System;

namespace MyDequeApp
{
    public class DequeProcessor<T>
    {
        private MyDeque<T> deque = new MyDeque<T>();

        public void AddToStart(T item)
        {
            deque.AddFirst(item);
        }

        public void AddToEnd(T item)
        {
            deque.AddLast(item);
        }

        public void RemoveStart()
        {
            var x = deque.RemoveFirst();
            Console.WriteLine("Удаляем первый: " + x);
        }

        public void RemoveEnd()
        {
            var x = deque.RemoveLast();
            Console.WriteLine("Удаляем последний: " + x);
        }

        public void Show()
        {
            deque.Print();
        }
    }
}
