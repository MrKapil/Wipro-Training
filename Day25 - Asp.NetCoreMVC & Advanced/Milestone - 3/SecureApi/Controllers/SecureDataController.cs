using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureApi.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SecureApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecureDataController : ControllerBase
{
    private readonly AppDbContext _db;
    public SecureDataController(AppDbContext db) => _db = db;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetSecureData()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { error = "Invalid token." });
        }

        var secure = await _db.SecureInfos.FirstOrDefaultAsync(s => s.UserId == userId);
        return Ok(new
        {
            message = "Secure data accessed successfully.",
            data = new
            {
                user_id = userId,
                secure_info = secure?.SecureInfoText ?? "No secure info found."
            }
        });
    }
}
