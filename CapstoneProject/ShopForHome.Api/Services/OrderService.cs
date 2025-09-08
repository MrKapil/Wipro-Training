using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Orders;
using ShopForHome.Api.Entities;

namespace ShopForHome.Api.Services
{
    public class OrderService
    {
        private readonly AppDbContext _db;
        private readonly StockService _stockSvc;
        public OrderService(AppDbContext db, StockService stockSvc) { _db = db; _stockSvc = stockSvc; }

        public async Task<OrderDto?> CheckoutAsync(int userId, CheckoutRequest req)
        {
            var cart = await _db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any()) return null;

            // calculate totals
            var total = cart.Items.Sum(i => (i.Product?.Price ?? 0) * i.Quantity);

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = total,
            };

            foreach (var ci in cart.Items)
            {
                order.Items.Add(new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product?.Price ?? 0
                });

                // reduce inventory (if exists)
                var inv = await _db.Inventory.FirstOrDefaultAsync(x => x.ProductId == ci.ProductId);
                if (inv != null)
                {
                    var old = inv.StockQty;
                    inv.StockQty = Math.Max(0, inv.StockQty - ci.Quantity);
                    var updated = inv.StockQty;
                    if (updated < 10 && old >= 10)
                    {
                        // register alert
                        _stockSvc.RegisterLowStockAlert(ci.ProductId, ci.Product?.Name ?? "Unknown", old, updated);
                    }
                }
            }

            _db.Orders.Add(order);

            // clear cart items
            _db.CartItems.RemoveRange(cart.Items);

            await _db.SaveChangesAsync();

            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? "",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task<List<OrderDto>> GetUserOrdersAsync(int userId)
        {
            return await _db.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Items = o.Items.Select(i => new OrderItemDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product!.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            return await _db.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Items = o.Items.Select(i => new OrderItemDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product!.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
