using System;

class A
{
    public int a;
    public int b;

    public A(int a, int b)
    {
        this.a = a;
        this.b = b;
    }

    public double CalcExpression()
    {
        return 1.0 / (a * a) - 1.0 / (b * b * b);
    }

    public int CubeSum()
    {
        int s = a + b;
        return s * s * s;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Введите a: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите b: ");
        int b = int.Parse(Console.ReadLine());

        A obj = new A(a, b);

        Console.WriteLine("\nРезультат выражения = " + obj.CalcExpression().ToString("F3"));

        Console.WriteLine("Куб суммы = " + obj.CubeSum());
    }
}
