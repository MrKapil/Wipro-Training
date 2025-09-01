using SecureApi.Models;

namespace SecureApi.Services;

public interface IUserService
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<string>> GetUserRolesAsync(User user);
}
