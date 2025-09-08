using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Auth;
using ShopForHome.Api.Helpers;
using ShopForHome.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        private readonly JwtTokenGenerator _jwt;
        public AuthController(AuthService auth, JwtTokenGenerator jwt) { _auth = auth; _jwt = jwt; }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _auth.ValidateCredentialsAsync(req.Email, req.Password);
            if (user == null) return Unauthorized(new { message = "Invalid credentials" });

            var (token, expires) = _jwt.GenerateToken(user);
            return Ok(new AuthResponse { Token = token, ExpiresAt = expires, Role = user.Role, FullName = user.FullName });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            try
            {
                var user = await _auth.RegisterAsync(req.FullName, req.Email, req.Password);
                var (token, expires) = _jwt.GenerateToken(user);
                return Ok(new AuthResponse { Token = token, ExpiresAt = expires, Role = user.Role, FullName = user.FullName });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
