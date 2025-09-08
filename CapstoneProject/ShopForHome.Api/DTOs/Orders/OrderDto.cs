namespace ShopForHome.Api.DTOs.Orders
{
      public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
