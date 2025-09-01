using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RepoDesignPattern.Models; // <-- use your actual namespace for the Employee class

namespace RepoDesignPattern.Data // <-- match your project namespace
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: enforce table name mapping via Fluent API (you already use [Table] attribute though)
            modelBuilder.Entity<Employee>().ToTable("Employee");
        }
    }
}