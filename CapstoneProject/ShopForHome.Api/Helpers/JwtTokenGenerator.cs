using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopForHome.Api.Entities;

namespace ShopForHome.Api.Helpers
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _cfg;
        public JwtTokenGenerator(IConfiguration cfg) { _cfg = cfg; }

        public (string token, DateTimeOffset expires) GenerateToken(User user)
        {
            var key = _cfg["Jwt:Key"] ?? throw new Exception("Jwt:Key missing");
            var issuer = _cfg["Jwt:Issuer"] ?? "ShopForHome";
            var audience = _cfg["Jwt:Audience"] ?? "ShopForHome";
            var expires = DateTimeOffset.UtcNow.AddHours(4);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "User"),
                new Claim("fullname", user.FullName ?? "")
            };

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, expires: expires.UtcDateTime, signingCredentials: creds);
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenStr, expires);
        }
    }
}
