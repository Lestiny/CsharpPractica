using System;
using System.Collections.Generic;

interface IPatientObserver
{
    void Notify(string msg);
}

class Patient : IPatientObserver
{
    public string pname;

    public Patient(string n)
    {
        pname = n;
    }

    public void Notify(string msg)
    {
        Console.WriteLine(pname + " получил уведомление: " + msg);
    }
}

class Clinic
{
    private List<IPatientObserver> list = new List<IPatientObserver>();

    public void AddPatient(IPatientObserver p)
    {
        list.Add(p);
    }

    public void RemovePatient(IPatientObserver p)
    {
        list.Remove(p);
    }

    public void Remind(string text)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].Notify(text);
        }
    }
}

class Program
{
    static void Main()
    {
        var clinic = new Clinic();

        var p1 = new Patient("Дарья");
        var p2 = new Patient("Александр");
        var p3 = new Patient("Марина");

        clinic.AddPatient(p1);
        clinic.AddPatient(p2);
        clinic.AddPatient(p3);

        clinic.Remind("Завтра у вас приём врача");
    }
}
