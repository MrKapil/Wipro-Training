using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.Entities;
using BCrypt.Net;

namespace ShopForHome.Api.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;
        public AuthService(AppDbContext db) { _db = db; }

        public async Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
            if (user == null) return null;
            bool ok = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return ok ? user : null;
        }

        public async Task<User> RegisterAsync(string fullName, string email, string password)
        {
            var exists = await _db.Users.AnyAsync(u => u.Email == email);
            if (exists) throw new System.Exception("Email already exists");

            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "User",
                IsActive = true
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            return newUser;
        }
    }
}
