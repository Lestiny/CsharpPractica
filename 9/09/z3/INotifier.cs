namespace NotificationSystem
{
    public interface INotifier<T>
    {
        void Notify(T message);
    }
}
