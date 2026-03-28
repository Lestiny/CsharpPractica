using System;

class UserIDGenerator
{
    private static UserIDGenerator instance;
    private int counter;

    private UserIDGenerator()
    {
        counter = 0;
    }

    public static UserIDGenerator GetInstance()
    {
        if (instance == null)
            instance = new UserIDGenerator();

        return instance;
    }

    public int GenerateID()
    {
        counter++;
        return counter;
    }
}

class Program
{
    static void Main()
    {
        var gen = UserIDGenerator.GetInstance();

        Console.WriteLine(gen.GenerateID());
        Console.WriteLine(gen.GenerateID());
        Console.WriteLine(gen.GenerateID());

        var gen2 = UserIDGenerator.GetInstance();
        Console.WriteLine(gen2.GenerateID());
    }
}
