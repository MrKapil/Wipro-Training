using System;

namespace CalculaterApp
{
    class CalculatorApp
    {
        public static void Main(string[] args)
        {
            int a;
            int b;

            Console.WriteLine("Enter THe First Number : ");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter The Seccond Number : ");
            b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"The Sum Of TWo Numbers Are : " + (a + b));
            Console.WriteLine($"The SubStraction of Two Number : " + (a - b));
            Console.WriteLine($"The Multiplication Of TWo Numbers Are : " + (a * b));
            Console.WriteLine($"The Division of Two Number : " + (a / b));
            Console.ReadKey();

        }
    }
}
