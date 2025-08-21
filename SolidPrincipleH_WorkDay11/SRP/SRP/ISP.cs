using System;

// Without Interface Segregation Principle (ISP) //

// Clients should not be forced to depend on interfaces they do not use. //




/*
public interface IWorker
{
    void Work();
    void Eat();
}

public class Robot : IWorker
{
    public void Work()
    {
        Console.WriteLine("Robot working...");
    }

    public void Eat()
    {
        throw new NotImplementedException();
    }
}

public class Program
{
    public static void Main()
    {
        IWorker robot = new Robot();
        robot.Work();
        // robot.Eat(); // Will throw exception
    }
}
*/


// With ISP //


/*
using System;

public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public class Human : IWorkable, IEatable
{
    public void Work() => Console.WriteLine("Human working...");
    public void Eat() => Console.WriteLine("Human eating...");
}

public class Robot : IWorkable
{
    public void Work() => Console.WriteLine("Robot working...");
}

public class Program
{
    public static void Main()
    {
        IWorkable humanWorker = new Human();
        IEatable humanEater = new Human();
        humanWorker.Work();
        humanEater.Eat();

        IWorkable robot = new Robot();
        robot.Work();
    }
}
*/