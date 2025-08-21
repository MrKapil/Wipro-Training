using System;

//Without - Liskov's Substitution Principle (LSP) //

// Subclasses should be substitutable for their base classes without breaking the behavior. \\
//Objects of a superclass should be replaceable with objects of a subclass without altering the correctness of the program.//



/*
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Flying");
    }
}

public class Ostrich : Bird
{
    public override void Fly()
    {
        throw new NotImplementedException("Ostrich can't fly!");
    }
}

public class Program
{
    public static void Main()
    {
        Bird bird = new Ostrich();
        bird.Fly(); // Throws error
    }
}
*/


//With LSP Principle

/*

public abstract class Bird { }

public class FlyingBird : Bird
{
    public void Fly()
    {
        Console.WriteLine("Flying");
    }
}

public class Ostrich : Bird
{
    public void Run()
    {
        Console.WriteLine("Running");
    }
}

public class Program
{
    public static void Main()
    {
        FlyingBird sparrow = new FlyingBird();
        sparrow.Fly();

        Ostrich ostrich = new Ostrich();
        ostrich.Run();
    }
}
*/