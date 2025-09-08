namespace ShopForHome.Api.Entities
{
    public class WishlistItem
    {
        public int WishlistItemId { get; set; }
        public int WishlistId { get; set; }
        public long ProductId { get; set; }

        // Navigation
        public Wishlist? Wishlist { get; set; }
        public Product? Product { get; set; }
    }
}
