using AdvancedRazorPages.Models;
using AdvancedRazorPages.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorPages.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _repo;
        public IEnumerable<Product> Products { get; private set; } = Enumerable.Empty<Product>();

        public IndexModel(IProductRepository repo) => _repo = repo;

        public void OnGet() => Products = _repo.GetAll();
    }
}
