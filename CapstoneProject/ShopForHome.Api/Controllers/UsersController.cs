using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.DTOs.Users;
using ShopForHome.Api.Services;
using System.Security.Claims;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _svc;
        public UsersController(UserService svc) { _svc = svc; }

        // Admin: list all users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _svc.GetAllAsync();
            return Ok(list);
        }

        // Admin or user: get single user (admin can get any; user can get own)
        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Get(long id)
        {
            var callerId = GetCallerUserId();
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && callerId != id) return Forbid();

            var user = await _svc.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // Admin: create user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
        {
            try
            {
                var created = await _svc.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = created.UserId }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Admin: update user
        [HttpPut("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, [FromBody] UserUpdateDto dto)
        {
            var ok = await _svc.UpdateAsync(id, dto);
            if (!ok) return NotFound();
            return NoContent();
        }

        // Admin: delete (soft)
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var ok = await _svc.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }

        private long GetCallerUserId()
        {
            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(idStr)) throw new Exception("User id missing in token");
            return long.Parse(idStr);
        }
    }
}
