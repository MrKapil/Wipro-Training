using AdvancedRazorPages.Models;
using AdvancedRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorPages.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductRepository _repo;

        public Product? Product { get; private set; }

        public DetailsModel(IProductRepository repo) => _repo = repo;

        public IActionResult OnGet(int id)
        {
            Product = _repo.GetById(id);
            if (Product is null) return NotFound();
            return Page();
        }
    }
}
