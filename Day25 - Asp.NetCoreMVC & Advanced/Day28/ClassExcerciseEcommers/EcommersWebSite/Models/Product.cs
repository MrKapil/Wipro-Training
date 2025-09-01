using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = "";
        [Range(1, 999999)] public decimal Price { get; set; }
        public string ImagePath { get; set; } = "/images/placeholder.png";
    }
}
