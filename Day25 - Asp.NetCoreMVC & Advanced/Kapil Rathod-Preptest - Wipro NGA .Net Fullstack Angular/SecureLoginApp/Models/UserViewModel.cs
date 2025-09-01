namespace SecureLoginApp.Models
{
    public class UserViewModel
    {
        public string Username { get; set; } = default!;
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
