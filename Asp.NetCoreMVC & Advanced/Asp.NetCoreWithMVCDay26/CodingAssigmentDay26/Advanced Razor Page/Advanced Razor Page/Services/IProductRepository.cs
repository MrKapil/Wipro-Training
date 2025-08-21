using AdvancedRazorPages.Models;

namespace AdvancedRazorPages.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        IEnumerable<Product> GetByCategory(string name);
        void Add(Product product);
    }
}
