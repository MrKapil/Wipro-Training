namespace EFCodeFirstDemo.Models;

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual StudentAddress Address { get; set; }
    }
