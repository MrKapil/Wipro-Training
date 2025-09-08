using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs.Users;
using ShopForHome.Api.Entities;

namespace ShopForHome.Api.Services
{
    public class UserService
    {
        private readonly AppDbContext _db;
        public UserService(AppDbContext db) { _db = db; }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _db.Users
                .Where(u => u.IsActive)
                .Select(u => new UserDto {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    IsActive = u.IsActive
                }).ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(long id)
        {
            var u = await _db.Users.FindAsync(id);
            if (u == null) return null;
            return new UserDto {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive
            };
        }

        public async Task<UserDto> CreateAsync(UserCreateDto dto)
        {
            if (await _db.Users.AnyAsync(x => x.Email == dto.Email))
                throw new Exception("Email already exists");

            var user = new User {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                IsActive = true
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return new UserDto {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public async Task<bool> UpdateAsync(long id, UserUpdateDto dto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return false;

            // soft-delete
            user.IsActive = false;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
