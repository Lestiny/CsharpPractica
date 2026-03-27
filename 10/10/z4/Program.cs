using System;
using System.IO;

class FileWatcher
{
    private FileSystemWatcher watcher;

    public FileWatcher(string path)
    {
        watcher = new FileSystemWatcher(path);
        watcher.IncludeSubdirectories = false;
        watcher.EnableRaisingEvents = true;

        watcher.Created += OnCreated;
        watcher.Deleted += OnDeleted;
        watcher.Changed += OnChanged;
        watcher.Renamed += OnRenamed;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine("Создан файл " + e.Name);
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine("Удалён файл " + e.Name);
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        try
        {
            FileInfo f = new FileInfo(e.FullPath);

            if (f.Exists && f.Length > 100 * 1024 * 1024)
            {
                Console.WriteLine("Файл " + e.Name + " слишком большой");
            }
            else
            {
                Console.WriteLine("Изменён файл " + e.Name);
            }
        }
        catch
        {
            Console.WriteLine("Ошибка при обработке изменений файла");
        }
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine("Файл переименован " + e.OldName + " -> " + e.Name);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Отслеживание изменений активировано");

        string folder = Directory.GetCurrentDirectory();
        FileWatcher fw = new FileWatcher(folder);

     
    }
}
