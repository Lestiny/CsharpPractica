using System;

interface IWeapon
{
    void Attack();
}

class Sword : IWeapon
{
    public void Attack()
    {
        Console.WriteLine("Меч: *взмах мечом*");
    }
}

class Bow : IWeapon
{
    public void Attack()
    {
        Console.WriteLine("Лук: *выстрел стрелой*");
    }
}

class Crossbow : IWeapon
{
    public void Attack()
    {
        Console.WriteLine("Арбалет: *стрела запущена*");
    }
}

abstract class WeaponFactory
{
    public abstract IWeapon Create();
}

class SwordFactory : WeaponFactory
{
    public override IWeapon Create()
    {
        return new Sword();
    }
}

class BowFactory : WeaponFactory
{
    public override IWeapon Create()
    {
        return new Bow();
    }
}

class CrossbowFactory : WeaponFactory
{
    public override IWeapon Create()
    {
        return new Crossbow();
    }
}

class Program
{
    static void Main()
    {
        WeaponFactory f1 = new SwordFactory();
        IWeapon w1 = f1.Create();
        w1.Attack();

        WeaponFactory f2 = new BowFactory();
        IWeapon w2 = f2.Create();
        w2.Attack();

        WeaponFactory f3 = new CrossbowFactory();
        IWeapon w3 = f3.Create();
        w3.Attack();
    }
}
