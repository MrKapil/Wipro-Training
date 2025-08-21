
// // create a delegate for a admin who is responsible for calculating the 
// //invoice(int tutionfess , int transportfees)
// //and one more delegate which will print the invoice
// using System;
// using System.Runtime.ExceptionServices;
// using static DelegateExample;
// namespace DelegateProject
// {


//     class DelegateExample
//     {
//         public delegate int Add(int tutionFees, int Transportfees);

//         public delegate void Invoice(int total);

//         static int Addnumber(int tutuionFees, int Transportfees)
//         {
//             return tutuionFees + Transportfees;
//         }

//         static void PrintInvoice(int total)
//         {
//             Console.WriteLine("The Total Invoice : " + total);
//         }

//         public static void Main(string[] args)
//         {
//             DelegateExample del = new DelegateExample();
//             DelegateExample.Add ad = Addnumber;
//             DelegateExample.Invoice i = PrintInvoice;
//             int total = del(10, 20);
//             Print(total);


//         }
//     }
// }