
using System.ComponentModel.DataAnnotations;

namespace AdvancedRazorPages.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        // Complex model binding: collection of Category
        public List<Category> Categories { get; set; } = new();
    }
}
