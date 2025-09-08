using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.DTOs.Categories;
using ShopForHome.Api.Services;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _svc;
        public CategoriesController(CategoryService svc) { _svc = svc; }

        // Public – list all categories
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = await _svc.GetAllAsync();
            return Ok(items);
        }

        // Admin – create new category
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CategoryId }, created);
        }

        // Admin – update category
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
        {
            var updated = await _svc.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // Admin – delete category (soft delete)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }

        // Optional – get by id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _svc.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }
    }
}
