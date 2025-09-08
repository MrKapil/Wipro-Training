using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Products;
using ShopForHome.Api.Entities;
using Microsoft.AspNetCore.Authorization;


namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db) => _db = db;

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] int? categoryId = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string? q = null)
        {
            var query = _db.Products
                .AsNoTracking()
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId.HasValue) query = query.Where(p => p.CategoryId == categoryId.Value);
            if (minPrice.HasValue) query = query.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice.Value);
            if (!string.IsNullOrWhiteSpace(q)) query = query.Where(p => p.Name.Contains(q) || p.Description.Contains(q));

            var items = await query
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
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

        // GET: api/products/{id}
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var p = await _db.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.ProductId == id && x.IsActive);

            if (p == null) return NotFound();

            return Ok(new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                SKU = p.SKU,
                Price = p.Price,
                Rating = p.Rating,
                ImageFileName = p.ImageFileName,
                Category = p.Category?.Name
            });
        }
        // POST: api/products (Admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductCreateUpdateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU,
                Price = dto.Price,
                Rating = dto.Rating,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                ImageFileName = dto.ImageFileName,
                IsActive = true
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
        }

        // PUT: api/products/{id} (Admin only)
        [HttpPut("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, ProductCreateUpdateDto dto)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = dto.Name;
            product.SKU = dto.SKU;
            product.Price = dto.Price;
            product.Rating = dto.Rating;
            product.Description = dto.Description;
            product.CategoryId = dto.CategoryId;
            product.ImageFileName = dto.ImageFileName;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/products/{id} (Admin only)
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.IsActive = false; // Soft delete
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
