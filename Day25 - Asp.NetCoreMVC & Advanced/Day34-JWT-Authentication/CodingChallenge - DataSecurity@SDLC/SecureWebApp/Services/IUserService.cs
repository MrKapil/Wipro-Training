using SecureWebApp.Models;

namespace SecureWebApp.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User user, string password);
        Task<User> AuthenticateAsync(string email, string password);
    }
}
