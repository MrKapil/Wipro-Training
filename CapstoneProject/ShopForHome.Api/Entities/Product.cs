using System;
using Microsoft.EntityFrameworkCore;

namespace ShopForHome.Api.Entities
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = "";
        public string SKU { get; set; } = "";
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Rating { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? ImageFileName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        // navigation
        public Inventory? Inventory { get; set; }
    }
}
