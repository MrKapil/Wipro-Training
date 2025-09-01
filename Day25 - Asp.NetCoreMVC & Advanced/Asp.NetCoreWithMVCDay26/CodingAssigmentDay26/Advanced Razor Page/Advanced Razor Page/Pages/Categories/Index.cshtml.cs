using AdvancedRazorPages.Models;
using AdvancedRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorPages.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _repo;

        public string Name { get; private set; } = string.Empty;
        public IEnumerable<Product> Products { get; private set; } = Enumerable.Empty<Product>();

        public IndexModel(IProductRepository repo) => _repo = repo;

        public IActionResult OnGet(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest();

            Name = name;
            Products = _repo.GetByCategory(name);
            return Page();
        }
    }
}
