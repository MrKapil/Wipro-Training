using System;
using System.Linq;
using BCrypt.Net;
using ShopForHome.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ShopForHome.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Ensure database exists (if using EF migrations skip this)
            try
            {
                context.Database.EnsureCreated();
            }
            catch { /*ignore*/ }

            // Seed categories
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Furniture", Slug = "furniture" },
                    new Category { Name = "Electronics", Slug = "electronics" },
                    new Category { Name = "Home & Kitchen", Slug = "home-kitchen" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // Seed admin user
            if (!context.Users.Any(u => u.Email == "admin@example.com"))
            {
                var pwdHash = BCrypt.Net.BCrypt.HashPassword("Admin@1234");
                var admin = new User
                {
                    FullName = "Admin User",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@1234"),
                    Role = "Admin",
                    IsActive = true
                };
                context.Users.Add(admin);
                context.SaveChanges();
            }

            // Seed products & inventory (only if none)
            if (!context.Products.Any())
            {
                var furniture = context.Categories.First(c => c.Slug == "furniture");
                var electronics = context.Categories.First(c => c.Slug == "electronics");

                var p1 = new Product {
                    Name = "Classic Wood Chair",
                    SKU = "CH-1001",
                    Description = "Comfortable wooden chair with cushion",
                    Price = 1299.00M,
                    Rating = 4.5M,
                    CategoryId = furniture.CategoryId,
                    ImageFileName = "chair-wood-01.jpg",
                    IsActive = true
                };
                var p2 = new Product {
                    Name = "Comfort Sofa (2-seater)",
                    SKU = "SF-2001",
                    Description = "Stylish 2-seater sofa",
                    Price = 8999.00M,
                    Rating = 4.2M,
                    CategoryId = furniture.CategoryId,
                    ImageFileName = "sofa-01.jpg",
                    IsActive = true
                };
                var p3 = new Product {
                    Name = "Sony 42 inch TV",
                    SKU = "TV-SONY-42",
                    Description = "42 inch Full HD LED TV",
                    Price = 24999.00M,
                    Rating = 4.6M,
                    CategoryId = electronics.CategoryId,
                    ImageFileName = "tv-sony-42.jpg",
                    IsActive = true
                };

                context.Products.AddRange(p1, p2, p3);
                context.SaveChanges();

                // Add inventory entries
                context.Inventory.AddRange(
                    new Inventory { ProductId = p1.ProductId, StockQty = 25 },
                    new Inventory { ProductId = p2.ProductId, StockQty = 10 },
                    new Inventory { ProductId = p3.ProductId, StockQty = 8 }
                );
                context.SaveChanges();
            }
        }
    }
}
