using System;

namespace NotificationSystem
{
    public class EmailNotifier<T> : INotifier<T>
    {
        public void Notify(T message)
        {
            Console.WriteLine("Email отправлен: " + message);
        }
    }
}
