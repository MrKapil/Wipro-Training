using System.ComponentModel.DataAnnotations;

namespace SecureTasks.ViewModels
{
    public class RegisterViewModel
    {
        [Required, StringLength(100)]
        public string FullName { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required, DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]).+$",
            ErrorMessage = "Must contain 1 uppercase, 1 number, 1 special char.")]
        public string Password { get; set; } = "";

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = "";
    }
}
