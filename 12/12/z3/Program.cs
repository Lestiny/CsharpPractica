using System;

interface ICommand
{
    void Execute();
}

class TaskScheduler
{
    public void StartTask()
    {
        Console.WriteLine("задача запущена");
    }

    public void CancelTask()
    {
        Console.WriteLine("задача отменена");
    }
}

class StartTaskCommand : ICommand
{
    TaskScheduler s;

    public StartTaskCommand(TaskScheduler sch)
    {
        s = sch;
    }

    public void Execute()
    {
        s.StartTask();
    }
}

class CancelTaskCommand : ICommand
{
    TaskScheduler s;

    public CancelTaskCommand(TaskScheduler sch)
    {
        s = sch;
    }

    public void Execute()
    {
        s.CancelTask();
    }
}

class SchedulerController
{
    private ICommand cmd;

    public void SetCommand(ICommand c)
    {
        cmd = c;
    }

    public void Run()
    {
        cmd.Execute();
    }
}

class Program
{
    static void Main()
    {
        var scheduler = new TaskScheduler();
        var controller = new SchedulerController();

        var start = new StartTaskCommand(scheduler);
        var cancel = new CancelTaskCommand(scheduler);

        controller.SetCommand(start);
        controller.Run();

        controller.SetCommand(cancel);
        controller.Run();
    }
}
