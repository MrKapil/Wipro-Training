namespace ShopForHome.Api.DTOs.Stock
{
    public class InventoryDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int StockQty { get; set; }
    }

    public class StockAlertDto
    {
        public string Id { get; set; } = "";
        public long ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int OldStock { get; set; }
        public int NewStock { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Acknowledged { get; set; }
    }
}
