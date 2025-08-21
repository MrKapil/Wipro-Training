using System.ComponentModel.DataAnnotations;

namespace RegistrationForm.Models
{
    public class Subscribe
    {
        [Required(ErrorMessage ="Id Required")]
        [StringLength(5, ErrorMessage ="Id Must be in Numbers")]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage ="Must Be less Than 50 Character")]
        public string name { get; set; }

        public string city { get; set; }

        public string gender { get; set; }

        public string Qualification { get; set; }



        [Required(ErrorMessage ="Password Rquired")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength =7, ErrorMessage ="Password must br a 7 character")]
        public string Password { get; set; }
    }
}
