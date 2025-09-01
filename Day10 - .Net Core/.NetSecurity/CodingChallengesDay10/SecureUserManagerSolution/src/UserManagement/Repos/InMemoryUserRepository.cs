using System.Collections.Concurrent;
using UserManagement.Models;

namespace UserManagement.Repos
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);

        public void Add(User user)
        {
            if (!_users.TryAdd(user.Username, user))
                throw new InvalidOperationException("User add failed (duplicate).");
        }

        public User? GetByUsername(string username)
            => _users.TryGetValue(username, out var u) ? u : null;
    }
}
