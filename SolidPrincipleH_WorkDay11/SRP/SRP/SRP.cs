using System;
using System.IO;


// Without - Single Responsibility Principle (SRP)

// A class should have only one reason to change, meaning it should have only one responsibility or job.//

// Class Student Manager Doing two jobs managing student data and write a file






/*
public class StudentManager
{
    public void AddStudent(string name)
    {
        Console.WriteLine($"Student Added: {name}");
        File.WriteAllText("log.txt", $"Added student: {name}"); // Violation
    }
}
class Program
{
    static void Main()
    {
        StudentManager sm = new StudentManager();
        sm.AddStudent("Kapil");
    }
}
*/




// With SRP Principle //

/*
public class StudentManager
{
    private Logger logger = new Logger();

    public void AddStudent(string name)
    {
        Console.WriteLine($"Student Added: {name}");
        logger.Log($"Added student: {name}");
    }
}

public class Logger
{
    public void Log(string message)
    {
        File.WriteAllText("log.txt", message);
    }
}

class SRP
{
    static void Main()
    {
        StudentManager sm = new StudentManager();
        sm.AddStudent("Kapil");
    }
}

*/