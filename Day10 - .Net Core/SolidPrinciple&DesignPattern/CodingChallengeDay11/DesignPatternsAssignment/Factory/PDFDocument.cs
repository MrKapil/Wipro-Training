using System;

namespace Factory
{
    public class PDFDocument : Document
    {
        public override void Print()
        {
            Console.WriteLine("Printing PDF Document");
        }
    }
}
