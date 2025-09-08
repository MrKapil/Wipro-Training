using System.Collections.Generic;

namespace ShopForHome.Api.Entities
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }

        // Navigation
        public User? User { get; set; }
        public ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}
