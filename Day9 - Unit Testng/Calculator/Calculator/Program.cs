using System;

namespace CalculatorTest
{
    public interface ICalculator
    {
        int Add(int a, int b);
        int Subtract(int a, int b);
        int Multiply(int a, int b);
        int Divide(int a, int b);
    }

    public class Calculator : ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Subtract(int a, int b)
        {
            return a - b;
        }
        public int Multiply(int a, int b)
        {
            return a * b;
        }
        public int Divide(int a, int b)
        {
            if(b == 0)
                throw new DivideByZeroException("Cannot divide by zero");
            return (int)a / b;
        }


        public static void Main(String[] args)
        {
            Calculator cla = new Calculator();
            cla.Add(2, 3);
            cla.Subtract(3, 2);
            cla.Multiply(2, 3);
            cla.Divide(4, 2);
        }

    }
}