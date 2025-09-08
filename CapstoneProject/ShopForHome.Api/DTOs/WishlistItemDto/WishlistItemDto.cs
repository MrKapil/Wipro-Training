namespace ShopForHome.Api.DTOs.Wishlist
{
    public class WishlistItemDto
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string ImageFileName { get; set; } = "";
    }
}
