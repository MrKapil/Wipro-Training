using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
    private readonly IConfiguration _configuration;
    public Startup(IConfiguration configuration) => _configuration = configuration;

    // No services required for this assignment, but keep the method for clarity/expansion.
    public void ConfigureServices(IServiceCollection services) { }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ===== Error handling: custom error page for unhandled exceptions =====
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "text/html";

                var file = Path.Combine(env.WebRootPath ?? "wwwroot", "error.html");
                var html = await File.ReadAllTextAsync(file);
                await context.Response.WriteAsync(html);
            });
        });

        // ===== Enforce HTTPS (security for all requests, including static files) =====
        if (!env.IsDevelopment())
        {
            app.UseHsts(); // adds Strict-Transport-Security header in non-dev
        }
        app.UseHttpsRedirection();

        // ===== Log requests & responses via custom middleware =====
        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        // ===== Serve static files from wwwroot =====
        // UseDefaultFiles lets "/" resolve to /index.html automatically.
        app.UseDefaultFiles();

        // Add security headers (CSP, etc.) for static files
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                var headers = ctx.Context.Response.Headers;

                // Basic, safe CSP that allows only local files.
                headers["Content-Security-Policy"] =
                    "default-src 'self'; script-src 'self'; style-src 'self'; img-src 'self' data:; " +
                    "object-src 'none'; base-uri 'self'; frame-ancestors 'none'; upgrade-insecure-requests";

                // Extra hardening
                headers["X-Content-Type-Options"] = "nosniff";
                headers["Referrer-Policy"] = "no-referrer";
                headers["Permissions-Policy"] = "geolocation=()";
            }
        });
    }
}
