using Microsoft.AspNetCore.Mvc;
using SecureApi.DTOs;
using SecureApi.Services;

namespace SecureApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService auth, ILogger<AuthController> logger)
    {
        _auth = auth;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var res = await _auth.LoginAsync(request);
        if (res == null)
            return Unauthorized(new { error = "Invalid username or password." });

        return Ok(res);
    }

    [HttpPost("oauth")]
    public async Task<IActionResult> OAuth([FromBody] OAuthRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var res = await _auth.OAuthLoginAsync(request.Provider, request.Token);
        if (res == null)
            return Unauthorized(new { error = "OAuth authentication failed." });

        return Ok(res);
    }
}
