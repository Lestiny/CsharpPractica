using System;

namespace TravelAgencySystem
{
    class Customer
    {
        public string Name;

        public Customer(string name)
        {
            Name = name;
        }
    }

    class Guide
    {
        public string Name;
        public string Language;

        public Guide(string name, string language)
        {
            Name = name;
            Language = language;
        }
    }

    class TourPackage
    {
        public string Country;
        public int Days;
        public int Price;

        public TourPackage(string country, int days, int price)
        {
            Country = country;
            Days = days;
            Price = price;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Страна: " + Country);
            Console.WriteLine("Дней: " + Days);
            Console.WriteLine("Цена: " + Price + " бел.руб");
        }
    }

    class TravelAgency
    {
        public string Name;

        public Guide[] Guides;

        public TourPackage Tour;

        public TravelAgency(string name, Guide[] guides, string country, int days, int price)
        {
            Name = name;
            Guides = guides;

            Tour = new TourPackage(country, days, price);
        }

        public void BookTour(Customer customer)
        {
            Console.WriteLine("Клиент " + customer.Name + " забронировал тур в агентстве " + Name);
            Tour.ShowInfo();

            Console.WriteLine("Доступные гиды для тура:");

            foreach (Guide guide in Guides)
            {
                Console.WriteLine("- " + guide.Name + " (" + guide.Language + ")");
            }

            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
    
            Guide g1 = new Guide("Лина", "Итальянский");
            Guide g2 = new Guide("Карина", "Русский");
            Guide g3 = new Guide("Ангелина", "Грузинский");

            TravelAgency[] agencies = new TravelAgency[2];

            agencies[0] = new TravelAgency(
                "XXXXXX",
                new Guide[] { g1, g2 },
                "Италия",
                10,
                6200
            );

            agencies[1] = new TravelAgency(
                "YYYYYY",
                new Guide[] { g2, g3 },
                "Грузия",
                5,
                4900
            );

            Customer c1 = new Customer("Александра");
            Customer c2 = new Customer("Павел");

            agencies[0].BookTour(c1);
            agencies[1].BookTour(c2);

            Console.ReadKey();
        }
    }
}