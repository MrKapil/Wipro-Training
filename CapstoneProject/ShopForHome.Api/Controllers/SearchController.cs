using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Products;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SearchController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string q = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (string.IsNullOrWhiteSpace(q)) return BadRequest("Query 'q' is required.");

            var items = await _db.Products
                .AsNoTracking()
                .Where(p => p.IsActive && (p.Name.Contains(q) || p.Description.Contains(q)))
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    SKU = p.SKU,
                    Price = p.Price,
                    Rating = p.Rating,
                    ImageFileName = p.ImageFileName,
                    Category = p.Category != null ? p.Category.Name : null
                })
                .ToListAsync();

            return Ok(items);
        }
    }
}
