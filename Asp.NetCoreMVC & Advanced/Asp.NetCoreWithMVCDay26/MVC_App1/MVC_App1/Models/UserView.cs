using System.ComponentModel.DataAnnotations;

namespace MVC_App1.Models
{
    public class UserView
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid emial format")]
        public string Email { get; set; }
    }
}
