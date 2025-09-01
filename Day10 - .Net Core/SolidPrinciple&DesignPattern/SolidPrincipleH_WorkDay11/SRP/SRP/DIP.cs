using System;

//WithOut - Dependency Inversion Principle (DIP) //

// High-level modules should not depend on low-level modules. Both should depend on abstractions (e.g., interfaces). //


/*
public class LightBulb
{
    public void TurnOn() => Console.WriteLine("Light On");
    public void TurnOff() => Console.WriteLine("Light Off");
}

public class Switch
{
    private LightBulb bulb = new LightBulb();

    public void Operate(string command)
    {
        if (command == "ON")
            bulb.TurnOn();
        else
            bulb.TurnOff();
    }
}

public class Program
{
    public static void Main()
    {
        Switch sw = new Switch();
        sw.Operate("ON");
        sw.Operate("OFF");
    }
}


*/


/*
// With - Dependency Inversion Principle (DIP) //

public interface ISwitchable
{
    void TurnOn();
    void TurnOff();
}

public class LightBulb : ISwitchable
{
    public void TurnOn() => Console.WriteLine("Light On");
    public void TurnOff() => Console.WriteLine("Light Off");
}

public class Switch
{
    private ISwitchable device;

    public Switch(ISwitchable device)
    {
        this.device = device;
    }

    public void Operate(string command)
    {
        if (command == "ON")
            device.TurnOn();
        else
            device.TurnOff();
    }
}

public class Program
{
    public static void Main()
    {
        ISwitchable bulb = new LightBulb();
        Switch sw = new Switch(bulb);
        sw.Operate("ON");
        sw.Operate("OFF");
    }
}
*/