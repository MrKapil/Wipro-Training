using UserManagement.Models;

namespace UserManagement.Repos
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetByUsername(string username);
    }
}
