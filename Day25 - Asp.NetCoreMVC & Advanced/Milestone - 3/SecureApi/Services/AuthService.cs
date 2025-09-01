using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecureApi.Data;
using SecureApi.DTOs;
using SecureApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecureApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _cfg;
    private readonly ILogger<AuthService> _logger;

    public AuthService(AppDbContext db, IConfiguration cfg, ILogger<AuthService> logger)
    {
        _db = db;
        _cfg = cfg;
        _logger = logger;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _db.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null)
        {
            _logger.LogWarning("Failed login attempt for non-existing user {Username}", request.Username);
            return null;
        }

        var valid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!valid)
        {
            _logger.LogWarning("Failed login attempt for user {Username}", request.Username);
            return null;
        }

        var token = GenerateJwt(user);
        var roles = user.UserRoles.Select(ur => ur.Role.Name).ToArray();

        return new AuthResponse
        {
            Token = token,
            ExpiresIn = int.Parse(_cfg["Jwt:ExpireMinutes"]) * 60,
            User = new { id = user.Id, username = user.Username, roles }
        };
    }

    public async Task<AuthResponse?> OAuthLoginAsync(string provider, string token)
    {
        if (provider.ToLowerInvariant() != "google")
        {
            _logger.LogWarning("Unsupported OAuth provider {Provider}", provider);
            return null;
        }

        var googleClientId = _cfg["Google:ClientId"];
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { googleClientId }
            });

            // payload contains email, name, sub, etc.
            var email = payload.Email ?? payload.Subject;
            var username = email.Split('@')[0];

            var user = await _db.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                // create new user
                user = new User
                {
                    Username = username,
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString()) // random strong password
                };
                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                // assign "User" role if exists
                var userRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "User");
                if (userRole != null)
                {
                    _db.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = userRole.Id });
                    await _db.SaveChangesAsync();
                }
            }

            // reload roles
            await _db.Entry(user).Collection(u => u.UserRoles).Query().Include(ur => ur.Role).LoadAsync();

            var jwt = GenerateJwt(user);
            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToArray();

            return new AuthResponse
            {
                Token = jwt,
                ExpiresIn = int.Parse(_cfg["Jwt:ExpireMinutes"]) * 60,
                User = new { id = user.Id, username = user.Username, roles }
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Google token validation failed");
            return null;
        }
    }

    private string GenerateJwt(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        // add roles as individual Role claims
        foreach (var role in user.UserRoles.Select(ur => ur.Role.Name))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: _cfg["Jwt:Issuer"],
            audience: _cfg["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_cfg["Jwt:ExpireMinutes"] ?? "60")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
