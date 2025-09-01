using System.ComponentModel.DataAnnotations;

namespace SecureApi.DTOs;

public class OAuthRequest
{
    [Required]
    public string Provider { get; set; } = null!; // e.g., "google"

    [Required]
    public string Token { get; set; } = null!; // provider access token / id token
}
