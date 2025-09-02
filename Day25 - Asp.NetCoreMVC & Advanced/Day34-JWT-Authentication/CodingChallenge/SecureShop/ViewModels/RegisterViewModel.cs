using System.ComponentModel.DataAnnotations;

namespace SecureShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&]).+$",
            ErrorMessage = "Password must contain 1 uppercase, 1 number, and 1 special char.")]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
