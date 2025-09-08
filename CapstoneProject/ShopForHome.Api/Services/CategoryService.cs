using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Categories;
using ShopForHome.Api.Entities;

namespace ShopForHome.Api.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext db) { _db = db; }

        // List all active categories
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await _db.Categories
                .Where(c => c.IsActive)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Slug = c.Slug
                })
                .ToListAsync();
        }

        // Get category by id
        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            return await _db.Categories
                .Where(c => c.CategoryId == id && c.IsActive)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Slug = c.Slug
                })
                .FirstOrDefaultAsync();
        }

        // Create new category
        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var entity = new Category
            {
                Name = dto.Name,
                Slug = dto.Slug,
                IsActive = true
            };
            _db.Categories.Add(entity);
            await _db.SaveChangesAsync();

            return new CategoryDto
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Slug = entity.Slug
            };
        }

        // Update category
        public async Task<CategoryDto?> UpdateAsync(int id, CategoryDto dto)
        {
            var entity = await _db.Categories.FindAsync(id);
            if (entity == null || !entity.IsActive) return null;

            entity.Name = dto.Name;
            entity.Slug = dto.Slug;
            await _db.SaveChangesAsync();

            dto.CategoryId = id;
            return dto;
        }

        // Soft delete category
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Categories.FindAsync(id);
            if (entity == null || !entity.IsActive) return false;

            entity.IsActive = false;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
