using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Register services
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();   // for API
        services.AddRazorPages();    // for Razor support if needed
    }

    // Configure middleware pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error.html");
            app.UseHsts();
        }

        // Serve static files (wwwroot)
        app.UseStaticFiles();

        // Custom logging middleware
        app.Use(async (context, next) =>
        {
            Console.WriteLine($"[{DateTime.Now}] Request: {context.Request.Method} {context.Request.Path}");
            await next.Invoke();
            Console.WriteLine($"[{DateTime.Now}] Response: {context.Response.StatusCode}");
        });

        app.UseRouting();

        // Configure endpoints
        app.UseEndpoints(endpoints =>
        {
            // Razor pages / controllers
            endpoints.MapControllers();
            endpoints.MapRazorPages();

            // Custom endpoints
            endpoints.MapGet("/api/hello", async context =>
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{ \"message\": \"Hello from API\" }");
            });

            endpoints.MapGet("/throw", context =>
            {
                throw new Exception("Test exception to show custom error page!");
            });
        });
    }
}
