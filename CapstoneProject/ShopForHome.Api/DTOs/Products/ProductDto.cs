namespace ShopForHome.Api.DTOs.Products
{
    public class ProductDto
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = "";
        public string SKU { get; set; } = "";
        public decimal Price { get; set; }
        public decimal? Rating { get; set; }
        public string? ImageFileName { get; set; }
        public string Category { get; set; } = "";
        public int StockQty { get; set; }
    }
}
