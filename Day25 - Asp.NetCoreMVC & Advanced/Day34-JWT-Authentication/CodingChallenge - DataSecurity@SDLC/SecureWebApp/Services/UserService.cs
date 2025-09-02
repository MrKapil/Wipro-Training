using Microsoft.EntityFrameworkCore;
using SecureWebApp.Data;
using SecureWebApp.Models;
using SecureWebApp.Utils;

namespace SecureWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            user.PasswordHash = PasswordHasher.HashPassword(password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }
    }
}
