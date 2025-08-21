using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class User
    {
        [Required, Display(Name="User name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{2,20}$", ErrorMessage = "Start with a letter; 3-21 chars.")]
        public string UserName { get; set; } = "";

        [Required, DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Min 4 characters.")]
        public string Password { get; set; } = "";
    }
}
