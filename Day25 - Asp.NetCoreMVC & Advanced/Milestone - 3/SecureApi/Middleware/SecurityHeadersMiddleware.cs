using Microsoft.AspNetCore.Http;

namespace SecureApi.Middleware;

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        
        if (httpContext.Request.IsHttps)
        {
            httpContext.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
        }

        httpContext.Response.Headers["Content-Security-Policy"] = "default-src 'self'";

        httpContext.Response.Headers["X-Content-Type-Options"] = "nosniff";
        httpContext.Response.Headers["X-Frame-Options"] = "DENY";
        httpContext.Response.Headers["Referrer-Policy"] = "no-referrer";

        await _next(httpContext);
    }
}
