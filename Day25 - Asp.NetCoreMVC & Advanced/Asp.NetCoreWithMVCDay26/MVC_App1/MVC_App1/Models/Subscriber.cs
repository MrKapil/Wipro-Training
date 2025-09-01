using System.ComponentModel.DataAnnotations;

namespace MVC_App1.Models
{
    public class Subscriber
    {
        [Required(ErrorMessage ="Username is Required")]
        [StringLength(50, ErrorMessage ="Username Must BeLess Than 50 Characters")]

        public string UserName { get; set; }

        [Required(ErrorMessage ="Email Id Required")]
        [EmailAddress(ErrorMessage ="Invalis Email Address")]

        public string Email { get; set; }

        [Required(ErrorMessage ="Password is Rquired")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength =6, ErrorMessage ="Password must be at least 6 character")]

        public string Password { get; set; }
    }
}
