namespace ShopForHome.Api.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        public User? User { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
