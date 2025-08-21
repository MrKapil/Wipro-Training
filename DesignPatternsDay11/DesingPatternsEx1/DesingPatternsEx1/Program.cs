using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesingPatternsEx1
{
    //public sealed class Singalton
    //{
    //    private static Singalton _instance = null;

    //    private Singalton() { }

    //    public static Singalton Instance
    //    {
    //        get
    //        {
    //            if (_instance == null)
    //                _instance = new Singalton();

    //            return _instance;
    //        }
    //    }


    //    public void ShowMessage(string msg)
    //    {
    //        Console.WriteLine(msg);
    //    }

    //    static void Main(string[] args)
    //    {
    //        //Singalton obj = new Singalton(); //this will not work as the Constructor is private
    //        Singalton obj = Singalton.Instance;
    //        obj.ShowMessage("Hello, Singleton Pattern");
    //    }
    //}

    //}



    // Adaptee: Legacy class with incompatible interface
    //    public class LegacyPrinter
    //    {
    //        public void PrintDocument()
    //        {
    //            Console.WriteLine("Printing document using LegacyPrinter...");
    //        }
    //    }

    //    // Target Interface: What the client expects
    //    public interface IPrinter
    //    {
    //        void Print();
    //    }

    //    // Adapter: Converts LegacyPrinter to IPrinter
    //    public class PrinterAdapter : IPrinter
    //    {
    //        private readonly LegacyPrinter _legacyPrinter;

    //        public PrinterAdapter(LegacyPrinter legacyPrinter)
    //        {
    //            _legacyPrinter = legacyPrinter;
    //        }

    //        public void Print()
    //        {
    //            _legacyPrinter.PrintDocument(); // Translate call
    //        }
    //    }

    //    // Client Code
    //    class Program
    //    {
    //        static void Main()
    //        {
    //            IPrinter printer = new PrinterAdapter(new LegacyPrinter());
    //            printer.Print(); // Works like a charm!
    //        }
    //    }




    /// <summary>
    using System;
    /// </summary>

    // Handler interface
    public abstract class Approver
    {
        protected Approver _next;

        public void SetNext(Approver next) => _next = next;

        public abstract void ProcessRequest(PurchaseRequest request);
    }

    public class Manager : Approver
    {
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount <= 10000)
                Console.WriteLine("Manager approved request for ₹" + request.Amount);
            else
                _next?.ProcessRequest(request);
        }
    }

    public class Director : Approver
    {
        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount <= 50000)
                Console.WriteLine("Director approved request for ₹" + request.Amount);
            else
                _next?.ProcessRequest(request);
        }
    }

    class Program
    {
        static void Main()
        {
            Approver manager = new Manager();
            Approver director = new Director();
            Approver vp = new VicePresident();

            manager.SetNext(director);
            director.SetNext(vp);

            var requests = new[]
            {
            new PurchaseRequest(5000),
            new PurchaseRequest(30000),
            new PurchaseRequest(75000),
            new PurchaseRequest(150000)
            };

            foreach (var request in requests)
            {
                manager.ProcessRequest(request);
            }
        }
    }



}
