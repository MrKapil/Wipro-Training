using Microsoft.AspNetCore.Mvc.RazorPages;
using RazerPageApplication.Services;
using System.Collections.Generic;

namespace RazerPageApplication.Pages
{
    public class ItemsModel : PageModel
    {
        private readonly ItemStore _store;

        public ItemsModel(ItemStore store)
        {
            _store = store;
        }

        public IReadOnlyList<Item> ItemList { get; set; }

        public void OnGet()
        {
            ItemList = _store.All;
        }
    }
}
