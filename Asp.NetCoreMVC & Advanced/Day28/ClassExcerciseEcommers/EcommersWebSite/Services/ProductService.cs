using EcommerceApp.Models;

namespace EcommerceApp.Services
{
    public class ProductService
    {
        private readonly List<Product> _products = new()
        {
            new() { Id=1, Price = 199, ImagePath="/images/p1.png"},
            new() { Id=2, Price = 299, ImagePath="/images/p2.png"},
            new() { Id=3, Price = 399, ImagePath="/images/p3.png"},
            new() { Id=4, Price = 099, ImagePath="/images/p4.png"},
        };

        public IEnumerable<Product> GetAll() => _products;
        public Product? Get(int id) => _products.FirstOrDefault(p => p.Id == id);
    }
}
