using System;

class Character
{
    public string Name;
    public string ClassName;
    public int Hp;
    public int Dmg;

    public override string ToString()
    {
        return Name + " [" + ClassName + "] HP=" + Hp + " DMG=" + Dmg;
    }
}

interface ICharacterBuilder
{
    void SetName(string n);
    void MakeStats();
    Character Get();
}

class WarriorBuilder : ICharacterBuilder
{
    Character ch = new Character();

    public void SetName(string n)
    {
        ch.Name = n;
        ch.ClassName = "Warrior";
    }

    public void MakeStats()
    {
        ch.Hp = 150;
        ch.Dmg = 20;
    }

    public Character Get()
    {
        return ch;
    }
}

class MageBuilder : ICharacterBuilder
{
    Character ch = new Character();

    public void SetName(string n)
    {
        ch.Name = n;
        ch.ClassName = "Mage";
    }

    public void MakeStats()
    {
        ch.Hp = 80;
        ch.Dmg = 40;
    }

    public Character Get()
    {
        return ch;
    }
}

class ArcherBuilder : ICharacterBuilder
{
    Character ch = new Character();

    public void SetName(string n)
    {
        ch.Name = n;
        ch.ClassName = "Archer";
    }

    public void MakeStats()
    {
        ch.Hp = 100;
        ch.Dmg = 25;
    }

    public Character Get()
    {
        return ch;
    }
}

class CharacterDirector
{
    private ICharacterBuilder b;

    public CharacterDirector(ICharacterBuilder builder)
    {
        b = builder;
    }

    public Character Build(string name)
    {
        b.SetName(name);
        b.MakeStats();
        return b.Get();
    }
}

class Program
{
    static void Main()
    {
        var d = new CharacterDirector(new WarriorBuilder());
        var w = d.Build("Thorin");
        Console.WriteLine(w);

        d = new CharacterDirector(new MageBuilder());
        var m = d.Build("Gandalf");
        Console.WriteLine(m);

        d = new CharacterDirector(new ArcherBuilder());
        var a = d.Build("Legolas");
        Console.WriteLine(a);
    }
}
