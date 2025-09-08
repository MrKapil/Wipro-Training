using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Cart;
using ShopForHome.Api.Entities;

namespace ShopForHome.Api.Services
{
    public class CartService
    {
        private readonly AppDbContext _db;
        public CartService(AppDbContext db) { _db = db; }

        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _db.Carts.Add(cart);
                await _db.SaveChangesAsync();
            }

            return new CartDto
            {
                CartId = cart.CartId,
                Items = cart.Items.Select(i => new CartItemDto
                {
                    CartItemId = i.CartItemId,
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? "",
                    Price = i.Product?.Price ?? 0,
                    Quantity = i.Quantity
                }).ToList()
            };
        }

        public async Task AddToCartAsync(int userId, AddToCartRequest req)
        {
            var cart = await _db.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _db.Carts.Add(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == req.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += req.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = req.ProductId,
                    Quantity = req.Quantity
                });
            }

            await _db.SaveChangesAsync();
        }

        public async Task<bool> UpdateCartItemAsync(int userId, UpdateCartRequest req)
        {
            var item = await _db.CartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.CartItemId == req.CartItemId && i.Cart!.UserId == userId);

            if (item == null) return false;

            item.Quantity = req.Quantity;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveItemAsync(int userId, int cartItemId)
        {
            var item = await _db.CartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.CartItemId == cartItemId && i.Cart!.UserId == userId);

            if (item == null) return false;

            _db.CartItems.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
