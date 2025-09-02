using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureJwtApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecureDataController : ControllerBase
    {
        [HttpGet("user")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult UserData()
        {
            return Ok("This is protected data for Users and Admins.");
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminData()
        {
            return Ok("This is protected data for Admins only.");
        }
    }
}
