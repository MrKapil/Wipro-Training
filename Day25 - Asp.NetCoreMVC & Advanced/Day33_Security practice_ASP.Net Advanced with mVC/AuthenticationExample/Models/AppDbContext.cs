using Microsoft.EntityFrameworkCore;
using AuthenticationManagemant.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthenticationManagemant.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

         public class AppDbContext : IdentityDbContext<AppUser>
        {
            public DbSet<Profile> Profiles { get; set; }
            public DbSet<ProfileBadge> ProfileBadges { get; set; }
            public DbSet<Badge> badges { get; set; }
            public DbSet<DeactivatedProfile> DeactivatedProfiles { get; set; }   
        }
    }
}
