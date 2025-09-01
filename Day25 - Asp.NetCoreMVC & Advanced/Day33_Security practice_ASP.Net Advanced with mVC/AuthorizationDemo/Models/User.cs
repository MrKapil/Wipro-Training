using System.ComponentModel.DataAnnotations;

namespace SimpleAuthDemo.Models
{
    public class User
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }  // (Plain for demo; in real apps hash it!)
    }
}
