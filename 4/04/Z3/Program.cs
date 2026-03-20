using System;

abstract class Pet
{
    public string Name;
    public string Breed;
    public int Age;
    public string OwnerName;

    protected Pet(string name, string breed, int age, string ownerName)
    {
        Name = name;
        Breed = breed;
        Age = age;
        OwnerName = ownerName;
    }

    public override string ToString()
    {
        return $"{Name}, {Breed}, {Age} лет, владелец: {OwnerName}";
    }
}

sealed class Dog : Pet
{
    public Dog(string name, string breed, int age, string ownerName)
        : base(name, breed, age, ownerName) { }
}

sealed class Cat : Pet
{
    public Cat(string name, string breed, int age, string ownerName)
        : base(name, breed, age, ownerName) { }
}

class VeterinaryClinic
{
    public Pet[] Pets;

    public VeterinaryClinic(Pet[] pets)
    {
        Pets = pets;
    }

    public Pet GetOldestPet()
    {
        Pet oldest = Pets[0];
        foreach (var p in Pets)
            if (p.Age > oldest.Age)
                oldest = p;
        return oldest;
    }

    public Pet[] GetPetsByOwner(string ownerName)
    {
        return Array.FindAll(Pets, p => p.OwnerName == ownerName);
    }
}

class Program
{
    static void Main()
    {
        Pet[] pets =
        {
            new Dog("Матильда", "Самоед", 4, "Камилла"),
            new Cat("Кольт", "Нибелунг", 3, "Камилла"),
            new Dog("Бобик", "Овчарка", 5, "Мария"),
            new Cat("Стайлз", "Шотландец", 7, "Катя")
        };

        VeterinaryClinic clinic = new VeterinaryClinic(pets);

        Console.WriteLine("Самое старое животное:");
        Console.WriteLine(clinic.GetOldestPet());

        Console.WriteLine("\nПитомцы Камиллы:");
        foreach (var p in clinic.GetPetsByOwner("Камилла"))
            Console.WriteLine(p);
    }
}
