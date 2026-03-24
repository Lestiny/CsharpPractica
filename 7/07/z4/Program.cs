using System;

class ServerShutdownManager
{
    public event EventHandler ServerShuttingDown;

    public void Shutdown()
    {
        ServerShuttingDown?.Invoke(this, EventArgs.Empty);
    }
}

class BackupService
{
    public void OnServerShutdown(object sender, EventArgs e)
    {
        Console.WriteLine("Создание резервной копии");
    }
}

class AlertSystem
{
    public void OnServerShutdown(object sender, EventArgs e)
    {
        Console.WriteLine("Администратор уведомлён");
    }
}

class ShutdownMonitor
{
    public ShutdownMonitor(ServerShutdownManager manager, BackupService backup, AlertSystem alert)
    {
        manager.ServerShuttingDown += backup.OnServerShutdown;
        manager.ServerShuttingDown += alert.OnServerShutdown;
    }
}

class Program
{
    static void Main()
    {
        var manager = new ServerShutdownManager();
        var backup = new BackupService();
        var alert = new AlertSystem();

        var monitor = new ShutdownMonitor(manager, backup, alert);

        manager.Shutdown();
    }
}
