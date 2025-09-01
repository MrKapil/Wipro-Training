using System;
using Singleton;
using Factory;
using Observer;

class Program
{
    static void Main()
    {
        Console.WriteLine("===== Singleton Pattern =====");
        Logger logger1 = Logger.GetInstance();
        Logger logger2 = Logger.GetInstance();

        logger1.Log("First log message");
        logger2.Log("Second log message");

        Console.WriteLine($"Are both loggers same instance? {logger1 == logger2}");
        Console.WriteLine();

        Console.WriteLine("===== Factory Pattern =====");
        Document pdf = DocumentFactory.CreateDocument("pdf");
        Document word = DocumentFactory.CreateDocument("word");

        pdf.Print();
        word.Print();
        Console.WriteLine();

        Console.WriteLine("===== Observer Pattern =====");
        WeatherStation station = new WeatherStation();

        WeatherDisplay display1 = new WeatherDisplay("Display 1");
        WeatherDisplay display2 = new WeatherDisplay("Display 2");

        station.RegisterObserver(display1);
        station.RegisterObserver(display2);

        station.SetWeatherData(30.5f, 65f);
        station.SetWeatherData(32f, 70f);

        station.RemoveObserver(display1);
        station.SetWeatherData(28f, 60f);

        Console.WriteLine("\nAll patterns demonstrated successfully!");
    }
}
