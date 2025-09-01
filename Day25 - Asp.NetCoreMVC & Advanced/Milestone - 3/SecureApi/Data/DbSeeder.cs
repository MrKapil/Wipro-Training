using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using SecureApi.Models;

namespace SecureApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db, IConfiguration config)
    {
        if (!db.Roles.Any())
        {
            var adminRole = new Role { Name = "Admin" };
            var userRole = new Role { Name = "User" };
            db.Roles.Add(adminRole);
            db.Roles.Add(userRole);
            await db.SaveChangesAsync();
        }

        if (!db.Users.Any(u => u.Username == "admin"))
        {
            var adminRole = db.Roles.First(r => r.Name == "Admin");
            var user = new User
            {
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123!") // change for production
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();

            db.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = adminRole.Id });
            await db.SaveChangesAsync();
        }

        if (!db.Users.Any(u => u.Username == "johndoe"))
        {
            var userRole = db.Roles.First(r => r.Name == "User");
            var user = new User
            {
                Username = "johndoe",
                Email = "johndoe@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("securePassword123!")
            };
            db.Users.Add(user);
            await db.SaveChangesAsync();

            db.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = userRole.Id });
            await db.SaveChangesAsync();

            db.SecureInfos.Add(new SecureInfo
            {
                UserId = user.Id,
                SecureInfoText = "This is some sensitive user data."
            });
            await db.SaveChangesAsync();
        }
    }
}
