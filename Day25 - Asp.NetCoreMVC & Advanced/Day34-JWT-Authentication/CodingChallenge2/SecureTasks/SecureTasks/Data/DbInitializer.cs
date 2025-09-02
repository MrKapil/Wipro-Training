using Microsoft.AspNetCore.Identity;
using SecureTasks.Models;

namespace SecureTasks.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = services.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = ["Admin", "User"];
            foreach (var r in roles)
                if (!await roleMgr.RoleExistsAsync(r))
                    await roleMgr.CreateAsync(new IdentityRole(r));

            // Admin
            var adminEmail = "admin@tasks.com";
            var admin = await userMgr.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, FullName = "Admin" };
                var res = await userMgr.CreateAsync(admin, "Admin@123!");
                if (res.Succeeded) await userMgr.AddToRoleAsync(admin, "Admin");
            }

            // Demo user with CanEditTask claim
            var userEmail = "user@tasks.com";
            var user = await userMgr.FindByEmailAsync(userEmail);
            if (user == null)
            {
                user = new ApplicationUser { UserName = userEmail, Email = userEmail, FullName = "Demo User" };
                var res = await userMgr.CreateAsync(user, "User@123!");
                if (res.Succeeded)
                {
                    await userMgr.AddToRoleAsync(user, "User");
                    await userMgr.AddClaimAsync(user, new System.Security.Claims.Claim("permission", "CanEditTask"));
                }
            }
        }
    }
}
