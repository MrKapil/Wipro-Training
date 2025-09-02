using Microsoft.EntityFrameworkCore;
using SecureWebApp.Models;

namespace SecureWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();
        }
    }
}
