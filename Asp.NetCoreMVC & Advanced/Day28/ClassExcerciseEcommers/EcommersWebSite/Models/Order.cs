namespace EcommerceApp.Models
{
    public class Order
    {
        public string OrderId { get; set; } = Guid.NewGuid().ToString("N")[..8].ToUpper();
        public string Customer { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<CartItem> Items { get; set; } = new();
        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}
