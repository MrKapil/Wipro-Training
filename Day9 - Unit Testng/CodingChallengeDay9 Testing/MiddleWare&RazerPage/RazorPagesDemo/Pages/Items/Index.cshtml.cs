using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace RazorPagesDemo.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly ItemService _itemService;

        public IndexModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public IEnumerable<string> Items { get; set; } = new List<string>();

        public void OnGet()
        {
            Items = _itemService.GetItems();
        }
    }
}
