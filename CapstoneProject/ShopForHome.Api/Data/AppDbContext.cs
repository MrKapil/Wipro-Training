using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Entities;

namespace ShopForHome.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Inventory> Inventory => Set<Inventory>();

        // Future DB sets:
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Wishlist> Wishlists => Set<Wishlist>();
        public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();


        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<CouponAssignment> CouponAssignments => Set<CouponAssignment>();

        // public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Unique indexes
            mb.Entity<User>().HasIndex(u => u.Email).IsUnique();
            mb.Entity<Product>().HasIndex(p => p.SKU).IsUnique();
            mb.Entity<Category>().HasIndex(c => c.Slug).IsUnique();

            // Decimal precision to avoid truncation
            mb.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            mb.Entity<Product>().Property(p => p.Rating).HasPrecision(3, 2);

            // Inventory PK as ProductId (one-to-one)
            mb.Entity<Inventory>().HasKey(i => i.ProductId);
            mb.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product -> Category relationship
            mb.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
