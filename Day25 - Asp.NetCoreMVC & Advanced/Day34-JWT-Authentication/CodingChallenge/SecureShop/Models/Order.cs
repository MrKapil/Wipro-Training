using System.ComponentModel.DataAnnotations;

namespace SecureShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    }
}
