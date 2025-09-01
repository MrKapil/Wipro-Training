using Microsoft.EntityFrameworkCore;
using AJAXwithMVC.Models;

namespace AJAXwithMVC.Models;

public class StudentContext : DbContext
{
    public DbSet<Student> Students { get; set; }
}
