using System;

// Without - Open/Closed Principle (OCP) //

// Software entities (like classes, modules, functions) should be open for extension but closed for modification.//

//

/*
public class Discount
{
    public double CalculateDiscount(string customerType, double amount)
    {
        if (customerType == "Regular")
            return amount * 0.1;
        else if (customerType == "Premium")
            return amount * 0.2;
        else
            return 0;   // -- Adding a new customer type means modifying the method.
    }
}

public class Program
{
    public static void Main()
    {
        Discount discount = new Discount();
        Console.WriteLine("Regular: " + discount.CalculateDiscount("Regular", 1000));
        Console.WriteLine("Premium: " + discount.CalculateDiscount("Premium", 1000));
    }
}

*/

//  With OCP Principle  //

public abstract class Discount
{
    public abstract double GetDiscount(double amount);
}

public class RegularCustomerDiscount : Discount                /// Implementation of Abstract Class
{
    public override double GetDiscount(double amount) => amount * 0.1;
}

public class PremiumCustomerDiscount : Discount     /// Implementation of Abstract Class here also     
{
    public override double GetDiscount(double amount) => amount * 0.2;
}



public class Program
{
    public static void Main()
    {
        Discount regular = new RegularCustomerDiscount();
        Discount premium = new PremiumCustomerDiscount();
        Console.WriteLine("Regular: " + regular.GetDiscount(1000));
        Console.WriteLine("Premium: " + premium.GetDiscount(1000));
    }
}
