using System.ComponentModel.DataAnnotations;

namespace SecureShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }
}
