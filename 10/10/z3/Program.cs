using System;
using System.Collections.Generic;
using System.IO;

class Deal
{
    public int Id;
    public int Revenue;
}

class DealFileReader
{
    private string path = "file.data";

    public List<Deal> ReadDeals()
    {
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "[]");
        }

        List<Deal> deals = new List<Deal>();

        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            string s = line.Trim();

            if (!s.StartsWith("{"))
                continue;

            s = s.Replace("{", "")
                 .Replace("}", "")
                 .Replace("\"", "")
                 .Replace(",", "");

            string[] parts = s.Split(' ');

            int id = 0;
            int revenue = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].StartsWith("Id:"))
                    id = int.Parse(parts[i + 1]);

                if (parts[i].StartsWith("Revenue:"))
                    revenue = int.Parse(parts[i + 1]);
            }

            deals.Add(new Deal() { Id = id, Revenue = revenue });
        }

        return deals;
    }
}

class DealProcessor
{
    public Deal FindMostProfitableDeal(List<Deal> deals)
    {
        if (deals.Count == 0)
            return null;

        Deal best = deals[0];

        foreach (var d in deals)
        {
            if (d.Revenue > best.Revenue)
                best = d;
        }

        return best;
    }
}

class Program
{
    static void Main()
    {
        DealFileReader reader = new DealFileReader();
        List<Deal> deals = reader.ReadDeals();

        DealProcessor processor = new DealProcessor();
        Deal best = processor.FindMostProfitableDeal(deals);

        if (best != null)
        {
            Console.WriteLine("Самая прибыльная сделка:");
            Console.WriteLine("Id: " + best.Id);
            Console.WriteLine("Revenue: " + best.Revenue);
        }
        else
        {
            Console.WriteLine("Сделок нет");
        }
    }
}
