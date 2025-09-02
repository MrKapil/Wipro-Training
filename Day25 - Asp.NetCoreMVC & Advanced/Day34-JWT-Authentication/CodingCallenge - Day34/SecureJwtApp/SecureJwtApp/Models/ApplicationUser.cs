using System.ComponentModel.DataAnnotations;

namespace SecureJwtApp.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        [Required]
        public string Role { get; set; } = "User"; // default role
    }
}
