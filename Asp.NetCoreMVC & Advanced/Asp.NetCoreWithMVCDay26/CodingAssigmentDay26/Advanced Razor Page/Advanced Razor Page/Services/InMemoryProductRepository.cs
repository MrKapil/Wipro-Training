using AdvancedRazorPages.Models;
using System;
using System.Linq;

namespace AdvancedRazorPages.Services
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();
        private int _nextId = 1;

        public IEnumerable<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.ProductID == id);

        public IEnumerable<Product> GetByCategory(string name) =>
            _products.Where(p => p.Categories.Any(c =>
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));

        public void Add(Product product)
        {
            product.ProductID = _nextId++;

            // Normalize categories: ignore blanks, assign simple IDs
            product.Categories = product.Categories
                .Where(c => !string.IsNullOrWhiteSpace(c.Name))
                .Select((c, i) => new Category { CategoryID = i + 1, Name = c.Name.Trim() })
                .ToList();

            _products.Add(product);
        }
    }
}
