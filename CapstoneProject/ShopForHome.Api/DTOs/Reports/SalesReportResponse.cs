namespace ShopForHome.Api.DTOs.Reports
{
    public class SalesReportResponse
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<TopProduct> TopProducts { get; set; } = new();
    }

    public class TopProduct
    {
        public string Name { get; set; } = "";
        public int QuantitySold { get; set; }
    }
}
