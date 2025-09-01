using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazerPageApplication.Services;

namespace RazerPageApplication.Pages
{
    public class AddItemModel : PageModel
    {
        private readonly ItemStore _store;

        public AddItemModel(ItemStore store)
        {
            _store = store;
        }

        [BindProperty]
        public string NewItemName { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(NewItemName))
            {
                _store.Add(new Item { Name = NewItemName });
            }

            return RedirectToPage("/Items");
        }
    }
}
