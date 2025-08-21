namespace EcommerceApp.Services
{
    public class UserService : IUserService
    {
        private static readonly Dictionary<string, string> _users =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["kapil"] = "1234",
                ["admin"] = "admin"
            };

        public bool Validate(string username, string password) =>
            _users.TryGetValue(username, out var pwd) && pwd == password;
    }
}
