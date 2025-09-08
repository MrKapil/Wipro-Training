using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Products;
using ShopForHome.Api.Entities;

namespace ShopForHome.Api.Services
{
    public class ProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db) { _db = db; }

        public async Task<List<ProductDto>> GetPagedAsync(int page, int pageSize)
        {
            return await _db.Products
                .Where(p => p.IsActive)
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
                    Category = p.Category != null ? p.Category.Name : "",
                    StockQty = p.Inventory != null ? p.Inventory.StockQty : 0
                })
                .ToListAsync();
        }

        public async Task<ProductDto?> GetByIdAsync(long id)
        {
            return await _db.Products
                .Where(p => p.ProductId == id && p.IsActive)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    SKU = p.SKU,
                    Price = p.Price,
                    Rating = p.Rating,
                    ImageFileName = p.ImageFileName,
                    Category = p.Category != null ? p.Category.Name : "",
                    StockQty = p.Inventory != null ? p.Inventory.StockQty : 0
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductDto> CreateAsync(ProductCreateUpdateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                SKU = dto.SKU,
                Description = dto.Description,
                Price = dto.Price,
                Rating = dto.Rating,
                CategoryId = dto.CategoryId,
                ImageFileName = dto.ImageFileName,
                IsActive = true
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            var inv = new Inventory { ProductId = product.ProductId, StockQty = dto.InitialStock };
            _db.Inventory.Add(inv);
            await _db.SaveChangesAsync();

            return await GetByIdAsync(product.ProductId) ?? throw new Exception("Product not created properly");
        }

        public async Task<ProductDto?> UpdateAsync(long id, ProductCreateUpdateDto dto)
        {
            var product = await _db.Products.Include(p => p.Inventory).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null) return null;

            product.Name = dto.Name;
            product.SKU = dto.SKU;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Rating = dto.Rating;
            product.CategoryId = dto.CategoryId;
            product.ImageFileName = dto.ImageFileName;

            if (product.Inventory != null)
                product.Inventory.StockQty = dto.InitialStock;

            await _db.SaveChangesAsync();
            return await GetByIdAsync(product.ProductId);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return false;

            product.IsActive = false;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
