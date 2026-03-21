using System;

abstract class Game
{
    public abstract void Play();

    public virtual void DisplayRules()
    {
        Console.WriteLine("Общие правила игры");
    }
}

class Chess : Game
{
    public override void Play()
    {
        Console.WriteLine("Игра в шахматы");
    }

    public override void DisplayRules()
    {
        Console.WriteLine("Правила: нужно поставить мат королю соперника.");
    }
}

class Checkers : Game
{
    public override void Play()
    {
        Console.WriteLine("Игра в шашки");
    }

    public override void DisplayRules()
    {
        Console.WriteLine("Правила: нужно побить все шашки соперника или заблокировать их.");
    }
}

class Program
{
    static void Main()
    {
        Game chess = new Chess();
        Game checkers = new Checkers();

        chess.Play();
        chess.DisplayRules();

        Console.WriteLine();

        checkers.Play();
        checkers.DisplayRules();
    }
}