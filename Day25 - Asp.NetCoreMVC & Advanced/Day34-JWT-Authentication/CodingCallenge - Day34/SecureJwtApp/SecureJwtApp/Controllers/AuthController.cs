using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureJwtApp.Data;
using SecureJwtApp.Models;
using SecureJwtApp.Services;
using System.Security.Cryptography;
using System.Text;

namespace SecureJwtApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _db.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("User already exists");

            var user = new ApplicationUser
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Role = "User"
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user);
            return Ok(new { token });
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private bool VerifyPassword(string password, string hash) =>
            HashPassword(password) == hash;
    }
}
