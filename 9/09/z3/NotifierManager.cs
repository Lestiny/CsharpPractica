using System;
using System.Collections.Generic;

namespace NotificationSystem
{
    public class NotifierManager<T>
    {
        private INotifier<T> notifier;

        public NotifierManager(INotifier<T> notifier)
        {
            this.notifier = notifier;
        }

        public void SendBulk(IEnumerable<T> messages)
        {
            foreach (var msg in messages)
            {
                notifier.Notify(msg);
            }
        }
    }
}
