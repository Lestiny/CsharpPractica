using System;

public partial class Athlete
{
    public string Name;
    public string Sport;
    public string Country;
    public bool Records;

    public Athlete(string name, string sport, string country, bool records)
    {
        Name = name;
        Sport = sport;
        Country = country;
        Records = records;
    }

    public override string ToString()
    {
        string rec = Records ? "Да" : "Нет";
        return $"{Name}, {Sport}, {Country}, Рекорд: {rec}";
    }
}

public partial class Athlete
{
    public bool IsInSport(string sport)
    {
        return Sport == sport;
    }

    public bool HasRecord()
    {
        return Records;
    }
}

class SportsEvent
{
    public Athlete[] Athletes;

    public SportsEvent(Athlete[] athletes)
    {
        Athletes = athletes;
    }

    public Athlete[] GetAthletesBySport(string sport)
    {
        return Array.FindAll(Athletes, a => a.IsInSport(sport));
    }

    public Athlete[] GetRecordHolders()
    {
        return Array.FindAll(Athletes, a => a.HasRecord());
    }
}

class Program
{
    static void Main()
    {
        Athlete[] athletes =
        {
            new Athlete("Кими", "Бег", "Италия", true),
            new Athlete("Сэди", "Плавание", "Испания", false),
            new Athlete("Макс", "Бег", "Великобритания", false),
            new Athlete("Тото", "Плавание", "Австрия", true)
        };

        SportsEvent ev = new SportsEvent(athletes);

        Console.WriteLine("Спортсмены по бегу:");
        foreach (var a in ev.GetAthletesBySport("Бег"))
            Console.WriteLine(a);

        Console.WriteLine("\nСпортсмены с рекордом:");
        foreach (var a in ev.GetRecordHolders())
            Console.WriteLine(a);
    }
}
