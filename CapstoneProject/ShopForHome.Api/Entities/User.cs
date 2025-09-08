using System;

namespace ShopForHome.Api.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Role { get; set; } = "User"; // or "Admin"
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    }
}
