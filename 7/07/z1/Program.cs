using System;

namespace CurrencyDelegateExample
{
    public delegate double CurrencyConverter(double amount);

    public class DollarToEuro
    {
        public double Convert(double dollars)
        {
            double rate = 0.92;
            return dollars * rate;
        }
    }

    public class EuroToYen
    {
        public double Convert(double euros)
        {
            double rate = 160.5;
            return euros * rate;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DollarToEuro d2e = new DollarToEuro();
            EuroToYen e2y = new EuroToYen();

            CurrencyConverter converter1 = new CurrencyConverter(d2e.Convert);
            CurrencyConverter converter2 = new CurrencyConverter(e2y.Convert);

            double dollars = 100;
            double euros = converter1(dollars);
            double yen = converter2(euros);

            Console.WriteLine("Доллары: " + dollars);
            Console.WriteLine("В евро: " + euros);
            Console.WriteLine("В йены: " + yen);

            Console.ReadLine();
        }
    }
}
