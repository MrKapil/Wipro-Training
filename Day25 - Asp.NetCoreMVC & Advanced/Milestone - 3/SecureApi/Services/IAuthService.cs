using SecureApi.DTOs;

namespace SecureApi.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    Task<AuthResponse?> OAuthLoginAsync(string provider, string token);
}
