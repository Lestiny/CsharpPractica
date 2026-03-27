using System;
using System.Collections.Generic;
using System.IO;

class Book
{
    public string Title;
    public string Author;
    public int Year;
}

class BookJsonWriter
{
    private string path = "file.data";

    public void WriteBooksAsJson(List<Book> books)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.WriteLine("[");
            for (int i = 0; i < books.Count; i++)
            {
                Book b = books[i];

                string json =
                    "  {" +
                    "\"Title\": \"" + b.Title + "\", " +
                    "\"Author\": \"" + b.Author + "\", " +
                    "\"Year\": " + b.Year +
                    "}";

                if (i < books.Count - 1)
                    json += ",";

                sw.WriteLine(json);
            }
            sw.WriteLine("]");
        }
    }
}

class Program
{
    static void Main()
    {
        List<Book> list = new List<Book>()
        {
            new Book(){ Title="Убийство в Восточном экспрессе", Author="Агата Кристи", Year=1934 },
            new Book(){ Title="Десять негритят", Author="Агата Кристи", Year=1939 },
            new Book(){ Title="Убийство Роджера Экройда", Author="Агата Кристи", Year=1926 }
        };

        BookJsonWriter writer = new BookJsonWriter();
        writer.WriteBooksAsJson(list);

        Console.WriteLine("Книги Агаты Кристи записаны в file.data");
    }
}
