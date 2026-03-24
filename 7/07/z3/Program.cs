using System;

delegate void WaterLevelHandler(int level);

class WaterTankSensor
{
    public event WaterLevelHandler WaterLevelChanged;

    public void SetLevel(int level)
    {
        WaterLevelChanged?.Invoke(level);
    }
}

class PumpController
{
    public void OnWaterLevelChanged(int level)
    {
        if (level < 30)
            Console.WriteLine("Насос включён");
        else
            Console.WriteLine("Насос выключен");
    }
}

class WarningSystem
{
    public void OnWaterLevelChanged(int level)
    {
        if (level > 80)
            Console.WriteLine("Почти полный");
    }
}

class Program
{
    static void Main()
    {
        var sensor = new WaterTankSensor();
        var pump = new PumpController();
        var warning = new WarningSystem();

        sensor.WaterLevelChanged += pump.OnWaterLevelChanged;
        sensor.WaterLevelChanged += warning.OnWaterLevelChanged;

        sensor.SetLevel(20);
        sensor.SetLevel(50);
        sensor.SetLevel(90);
    }
}
