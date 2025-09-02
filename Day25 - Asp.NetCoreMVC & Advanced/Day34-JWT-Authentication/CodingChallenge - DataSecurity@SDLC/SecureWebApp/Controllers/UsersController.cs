using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Models;
using SecureWebApp.Services;

namespace SecureWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user, string password)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.RegisterAsync(user, password);
            return Ok(new { result.Id, result.Name, result.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _userService.AuthenticateAsync(login.Email, login.PasswordHash);
            if (user == null) return Unauthorized("Invalid credentials");

            return Ok(new { user.Id, user.Name, user.Email });
        }
    }
}
