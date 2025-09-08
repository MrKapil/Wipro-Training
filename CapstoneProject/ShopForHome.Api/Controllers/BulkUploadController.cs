using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.Data;
using ShopForHome.Api.Entities;
using ShopForHome.Api.Helpers;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class BulkUploadController : ControllerBase
    {
        private readonly AppDbContext _db;
        public BulkUploadController(AppDbContext db) { _db = db; }

        [HttpPost("products")]
        [RequestSizeLimit(10_000_000)] // 10 MB max
        public async Task<IActionResult> UploadProducts(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var parsed = CsvParser.Parse(file.OpenReadStream());
            var products = new List<Product>();

            foreach (var row in parsed)
            {
                try
                {
                    var product = new Product
                    {
                        Name = row.GetValueOrDefault("Name") ?? "Unnamed",
                        SKU = row.GetValueOrDefault("SKU") ?? Guid.NewGuid().ToString(),
                        Description = row.GetValueOrDefault("Description") ?? "",
                        Price = decimal.TryParse(row.GetValueOrDefault("Price"), out var price) ? price : 0,
                        Rating = decimal.TryParse(row.GetValueOrDefault("Rating"), out var rating) ? rating : 0,
                        ImageFileName = row.GetValueOrDefault("ImageFileName") ?? "",
                        CategoryId = int.TryParse(row.GetValueOrDefault("CategoryId"), out var catId) ? catId : 1,
                        IsActive = true
                    };
                    products.Add(product);
                }
                catch
                {
                    // Skip malformed rows
                }
            }

            _db.Products.AddRange(products);
            await _db.SaveChangesAsync();

            return Ok(new { Count = products.Count });
        }
    }
}
