using System.ComponentModel.DataAnnotations;
namespace JWTAuthenticationDemo.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]

        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
        
        public string? EmailAddress { get; set; }  
    }
}