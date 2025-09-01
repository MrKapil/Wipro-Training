using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureLoginApp.Models;

namespace SecureLoginApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            
            await context.Database.MigrateAsync();

            
            string[] roles = new[] { "Admin", "User" };
            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole(r));
            }

           
            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                    FullName = "Administrator"
                };
                var adminPwd = "Admin@123";
                var result = await userManager.CreateAsync(adminUser, adminPwd);
                if (result.Succeeded) await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            
            var user1 = await userManager.FindByNameAsync("user1");
            if (user1 == null)
            {
                user1 = new ApplicationUser
                {
                    UserName = "user1",
                    Email = "user1@example.com",
                    EmailConfirmed = true,
                    FullName = "Regular User"
                };
                var userPwd = "User@123";
                var result = await userManager.CreateAsync(user1, userPwd);
                if (result.Succeeded) await userManager.AddToRoleAsync(user1, "User");
            }
        }
    }
}
