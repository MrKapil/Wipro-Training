namespace SecureApi.DTOs;

public class AuthResponse
{
    public string Token { get; set; } = null!;
    public int ExpiresIn { get; set; }
    public object User { get; set; } = null!;
}
