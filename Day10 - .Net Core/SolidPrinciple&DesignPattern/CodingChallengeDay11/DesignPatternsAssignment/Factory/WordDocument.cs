using System;

namespace Factory
{
    public class WordDocument : Document
    {
        public override void Print()
        {
            Console.WriteLine("Printing Word Document");
        }
    }
}
