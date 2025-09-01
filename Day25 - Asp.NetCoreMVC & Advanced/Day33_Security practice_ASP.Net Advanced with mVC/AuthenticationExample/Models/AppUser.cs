using Microsoft.Asp.NetCore.Identity;

namespace AuthenticationManagemant.Models
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }
    }
}