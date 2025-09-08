using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Reports;

namespace ShopForHome.Api.Services
{
    public class ReportService
    {
        private readonly AppDbContext _db;
        public ReportService(AppDbContext db) { _db = db; }

        public async Task<SalesReportResponse> GenerateAsync(DateTime from, DateTime to)
        {
            var orders = await _db.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.OrderDate >= from && o.OrderDate <= to)
                .ToListAsync();

            var totalOrders = orders.Count;
            var totalRevenue = orders.Sum(o => o.TotalAmount);

            var topProducts = orders
                .SelectMany(o => o.Items)
                .GroupBy(i => i.Product!.Name)
                .Select(g => new TopProduct
                {
                    Name = g.Key,
                    QuantitySold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToList();

            return new SalesReportResponse
            {
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                TopProducts = topProducts
            };
        }
    }
}
