using System.Text.Json;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommerceApp.Pages.Billing
{
    public class IndexModel : PageModel
    {
        [BindProperty] public Order? LastOrder { get; set; }

        public IActionResult OnGet()
        {
            if (TempData["LastOrder"] is string json)
            {
                LastOrder = JsonSerializer.Deserialize<Order>(json);
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
