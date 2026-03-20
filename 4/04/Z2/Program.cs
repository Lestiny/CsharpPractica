using System;

class Person
{
    public string Name;
    public int Age;

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"{Name}, {Age}";
    }
}

static class PersonArrayUtils
{
    public static void SortByAge(Person[] arr)
    {
        Array.Sort(arr, (p1, p2) => p1.Age.CompareTo(p2.Age));
    }

    public static Person[] FilterAdults(Person[] arr)
    {
        return Array.FindAll(arr, p => p.Age >= 18);
    }

    public static double AverageAge(Person[] arr)
    {
        int sum = 0;
        foreach (var p in arr)
            sum += p.Age;
        return (double)sum / arr.Length;
    }

    public static void ReverseArray(Person[] arr)
    {
        Array.Reverse(arr);
    }
}

class Program
{
    static void Main()
    {
        Person[] people =
        {
            new Person("Дарья", 17),
            new Person("Яна", 16),
            new Person("Камилла", 19),
            new Person("Лина", 22)
        };

        Console.WriteLine("Исходный массив:");
        foreach (var p in people)
            Console.WriteLine(p);

        PersonArrayUtils.ReverseArray(people);

        Console.WriteLine("\nИзменение порядка обратно:");
        foreach (var p in people)
            Console.WriteLine(p);

        PersonArrayUtils.SortByAge(people);

        Console.WriteLine("\nСортировка по возрасту:");
        foreach (var p in people)
            Console.WriteLine(p);

        var adults = PersonArrayUtils.FilterAdults(people);

        Console.WriteLine("\nВзрослые:");
        foreach (var p in adults)
            Console.WriteLine(p);

        Console.WriteLine("\nСредний возраст: " + PersonArrayUtils.AverageAge(people));
    }
}
