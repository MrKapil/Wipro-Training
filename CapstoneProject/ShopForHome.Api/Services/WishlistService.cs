using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Wishlist;
using ShopForHome.Api.Entities;
using ShopForHome.Api.DTOs.Categories;

namespace ShopForHome.Api.Services
{
    public class WishlistService
    {
        private readonly AppDbContext _db;
        public WishlistService(AppDbContext db) { _db = db; }

        public async Task<List<WishlistItemDto>> GetWishlistAsync(int userId)
        {
            return await _db.WishlistItems
                .Include(wi => wi.Product)
                .Where(wi => wi.Wishlist.UserId == userId)
                .Select(wi => new WishlistItemDto
                {
                    ProductId = wi.ProductId,
                    Name = wi.Product.Name,
                    Price = wi.Product.Price,
                    ImageFileName = wi.Product.ImageFileName
                })
                .ToListAsync();
        }

        public async Task<bool> AddToWishlistAsync(int userId, long productId)
        {
            var wishlist = await _db.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist { UserId = userId, Items = new List<WishlistItem>() };
                _db.Wishlists.Add(wishlist);
                await _db.SaveChangesAsync();
            }

            var exists = await _db.WishlistItems.AnyAsync(wi => wi.WishlistId == wishlist.WishlistId && wi.ProductId == productId);
            if (exists) return false; // already added

            var item = new WishlistItem { WishlistId = wishlist.WishlistId, ProductId = productId };
            _db.WishlistItems.Add(item);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, long productId)
        {
            var item = await _db.WishlistItems
                .Include(wi => wi.Wishlist)
                .FirstOrDefaultAsync(wi => wi.Wishlist.UserId == userId && wi.ProductId == productId);

            if (item == null) return false;

            _db.WishlistItems.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task ClearWishlistAsync(int userId)
        {
            var items = _db.WishlistItems.Where(wi => wi.Wishlist.UserId == userId);
            _db.WishlistItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }
    }
}
