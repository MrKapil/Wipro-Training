namespace SecureLoginApp.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; } = default!;
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Message { get; set; }
    }
}
