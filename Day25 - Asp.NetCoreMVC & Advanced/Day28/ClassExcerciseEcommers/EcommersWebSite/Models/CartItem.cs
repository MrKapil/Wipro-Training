using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public decimal UnitPrice { get; set; }

        [Range(1, 99, ErrorMessage = "Quantity 1–99")]
        public int Quantity { get; set; } = 1;

        public decimal Subtotal => UnitPrice * Quantity;
    }
}
