// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Runtime.CompilerServices;
// using System.Text;
// using System.Threading.Tasks;
// using ConsoleApp2;
// //Extension Method - which allows us to add new methods into a class without editing the source code of 
// //the class ;

// /* For eg: if a class has some methods and in future if developers wants to add some more methods but 
//  they do not permission of accessing the class*/
// namespace ConsoleApp2
// {
//    public sealed class OldService
//     {

//         public int x = 300;
//         public void Test1()
//         {
//             Console.WriteLine("Test 1 method created :");
//         }
//         public void Test2()
//         {
//             Console.WriteLine("Test 2 method created :");
//         }

        
//     }
// }


// // NotificationService ns = Email;
// // ns += sms;


// // using System;
// // using System.Collections.Generic;
// // using System.Linq;
// // using System.Text;
// // using System.Threading.Tasks;
// // using ConsoleApp2;


// public static class NewService
// {
//     static int x1 = 200;
//     public static void Test4(this OldService ser)
//     {
//         Console.WriteLine("New extension Method :");
//     }

//     public static void Test5(this OldService ser1, int x,int y)
//     {
//         int result = x + y;
//         Console.WriteLine(result);
//         Console.WriteLine(ser1.x);
//     }

//     public static bool IsPalindrome(this string s)
//     {

//        var rev = new string(s.Reverse().ToArray());

//         return s.Equals(rev, StringComparison.OrdinalIgnoreCase);
    
//     }
//     public static void Main()
//     {

//         OldService sobj = new OldService();
//         sobj.Test1();
//         sobj.Test2();
//         sobj.Test4();
//         sobj.Test5(600,700);
//         string s = "Madam";
//         Console.WriteLine( s.IsPalindrome());



//     }
// }