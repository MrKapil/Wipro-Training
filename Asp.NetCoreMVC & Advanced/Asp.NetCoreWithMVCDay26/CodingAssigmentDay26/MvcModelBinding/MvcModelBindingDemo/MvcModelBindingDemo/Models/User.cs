namespace MvcModelBindingDemo.Models
{
    // Represents the user (simple types + nested complex Address)
    public class User
    {
        // Simple types
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }

        // Complex (nested) type
        public Address? Address { get; set; }
    }
}
