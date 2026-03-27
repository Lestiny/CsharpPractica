using System;
using System.IO;

class FileManager
{
    public void CreateFile(string path, string text)
    {
        File.WriteAllText(path, text);
    }

    public void DeleteFile(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
        else
            throw new FileNotFoundException();
    }

    public void CopyFile(string source, string dest)
    {
        File.Copy(source, dest, true);
    }

    public void MoveFile(string source, string dest)
    {
        File.Move(source, dest);
    }

    public void RenameFile(string path, string newName)
    {
        string dir = Path.GetDirectoryName(path);
        string newPath = Path.Combine(dir, newName);
        File.Move(path, newPath);
    }

    public void DeleteByPattern(string dir, string pattern)
    {
        string[] files = Directory.GetFiles(dir, pattern);
        foreach (var f in files)
            File.Delete(f);
    }

    public string[] ListFiles(string dir)
    {
        return Directory.GetFiles(dir);
    }
}

class FileInfoProvider
{
    public long GetSize(string path)
    {
        return new FileInfo(path).Length;
    }

    public DateTime GetCreated(string path)
    {
        return File.GetCreationTime(path);
    }

    public DateTime GetModified(string path)
    {
        return File.GetLastWriteTime(path);
    }

    public void CheckAccess(string path)
    {
        FileInfo f = new FileInfo(path);
        Console.WriteLine(f.IsReadOnly ? "Файл только для чтения" : "Запись разрешена");
    }
}

class Program
{
    static void Main()
    {
        string file = "korol.ka";
        string copy = "korol_copy.ka";
        string moved = "moved_korol.ka";
        string dir = Directory.GetCurrentDirectory();

        var fm = new FileManager();
        var info = new FileInfoProvider();

        fm.CreateFile(file, "Привет");
        Console.WriteLine(File.ReadAllText(file));

        if (File.Exists(file))
            fm.DeleteFile(file);
        fm.CreateFile(file, "Новый текст после удаления");

        Console.WriteLine(info.GetSize(file));
        Console.WriteLine(info.GetCreated(file));
        Console.WriteLine(info.GetModified(file));

        fm.CopyFile(file, copy);
        Console.WriteLine(File.Exists(copy) ? "Копия создана" : "Копия не найдена");

        fm.MoveFile(copy, moved);

        fm.RenameFile(file, "familiya.io");

        try
        {
            fm.DeleteFile("несуществующий_файл.ka");
        }
        catch
        {
            Console.WriteLine("Файл не найден");
        }

        long s1 = info.GetSize("familiya.io");
        long s2 = info.GetSize(moved);
        Console.WriteLine(s1 == s2 ? "Файлы одинаковые" : "Файлы разные");

        fm.DeleteByPattern(dir, "*.ka");

        foreach (var f in fm.ListFiles(dir))
            Console.WriteLine(f);

        File.SetAttributes("familiya.io", FileAttributes.ReadOnly);
        try
        {
            File.WriteAllText("familiya.io", "Попытка записи");
        }
        catch
        {
            Console.WriteLine("Запись запрещена");
        }

        info.CheckAccess("familiya.io");
    }
}
