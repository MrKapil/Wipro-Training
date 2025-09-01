using Microsoft.EntityFrameworkCore;
using SecureApi.Data;
using SecureApi.Models;

namespace SecureApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    public UserService(AppDbContext db) => _db = db;

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _db.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<IEnumerable<string>> GetUserRolesAsync(User user)
    {
        var roles = user.UserRoles.Select(ur => ur.Role.Name);
        return Task.FromResult(roles);
    }
}
