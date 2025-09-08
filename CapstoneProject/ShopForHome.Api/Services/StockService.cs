using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Stock;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace ShopForHome.Api.Services
{
    public class StockService
    {
        private readonly AppDbContext _db;

        // in-memory alerts (simple solution). Keeps alerts until app restarts.
        private static ConcurrentDictionary<string, StockAlertDto> _alerts = new();

        public StockService(AppDbContext db) { _db = db; }

        public async Task<List<InventoryDto>> GetAllInventoryAsync()
        {
            return await _db.Inventory
                .Include(i => i.Product)
                .Select(i => new InventoryDto {
                    ProductId = i.ProductId,
                    ProductName = i.Product!.Name,
                    StockQty = i.StockQty
                }).ToListAsync();
        }

        public async Task<List<InventoryDto>> GetLowStockAsync(int threshold = 10)
        {
            return await _db.Inventory
                .Include(i => i.Product)
                .Where(i => i.StockQty <= threshold)
                .Select(i => new InventoryDto {
                    ProductId = i.ProductId,
                    ProductName = i.Product!.Name,
                    StockQty = i.StockQty
                }).ToListAsync();
        }

        // Called by OrderService when inventory is reduced
        public void RegisterLowStockAlert(long productId, string productName, int oldStock, int newStock)
        {
            // create simple alert
            var alert = new StockAlertDto {
                Id = Guid.NewGuid().ToString(),
                ProductId = productId,
                ProductName = productName,
                OldStock = oldStock,
                NewStock = newStock,
                Timestamp = DateTime.UtcNow,
                Acknowledged = false
            };
            _alerts[alert.Id] = alert;
            // also write to console so developer/admin can see it in logs
            Console.WriteLine($"[LOW-STOCK] {productName} ({productId}) old={oldStock} new={newStock}");
        }

        public List<StockAlertDto> GetAlerts() => _alerts.Values.OrderByDescending(a => a.Timestamp).ToList();

        public bool AcknowledgeAlert(string id)
        {
            if (_alerts.TryGetValue(id, out var a))
            {
                a.Acknowledged = true;
                _alerts[id] = a;
                return true;
            }
            return false;
        }
    }
}
