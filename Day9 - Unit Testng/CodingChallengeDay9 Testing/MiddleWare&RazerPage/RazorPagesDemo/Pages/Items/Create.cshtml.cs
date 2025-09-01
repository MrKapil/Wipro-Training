using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesDemo.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly ItemService _itemService;

        public CreateModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        [BindProperty]
        public string NewItem { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnPost()
        {
            if (!string.IsNullOrWhiteSpace(NewItem))
            {
                _itemService.AddItem(NewItem);
                Message = $"Item '{NewItem}' added successfully!";
            }
            else
            {
                Message = "Please enter a valid item.";
            }
        }
    }
}
