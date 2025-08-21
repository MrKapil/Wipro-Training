using AdvancedRazorPages.Models;
using AdvancedRazorPages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdvancedRazorPages.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _repo;

        [BindProperty]
        public Product Product { get; set; } = new();

        public CreateModel(IProductRepository repo) => _repo = repo;

        public void OnGet()
        {
            // Start with one empty category row for convenience
            if (Product.Categories.Count == 0)
                Product.Categories.Add(new Category());
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _repo.Add(Product);
            return RedirectToPage("/Products/Index");
        }
    }
}
