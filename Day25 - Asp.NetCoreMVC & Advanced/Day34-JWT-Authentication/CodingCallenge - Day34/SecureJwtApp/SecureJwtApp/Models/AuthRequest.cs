using System.ComponentModel.DataAnnotations;

namespace SecureJwtApp.Models
{
    public class AuthRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}

