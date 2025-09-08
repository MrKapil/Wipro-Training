namespace ShopForHome.Api.DTOs.Products
{
    public class ProductCreateUpdateDto
    {
        public string Name { get; set; } = "";
        public string SKU { get; set; } = "";
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Rating { get; set; }
        public int CategoryId { get; set; }
        public string? ImageFileName { get; set; }
        public int InitialStock { get; set; }
    }
}
