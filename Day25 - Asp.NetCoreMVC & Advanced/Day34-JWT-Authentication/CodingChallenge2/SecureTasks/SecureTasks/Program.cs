using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureTasks.Data;
using SecureTasks.Models;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity (forms auth with secure cookie)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Strong password rules (hashed+salted by Identity)
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Secure cookie/session settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".SecureTasks.Auth";
    options.Cookie.HttpOnly = true;                 // helps mitigate XSS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS only
    options.Cookie.SameSite = SameSiteMode.Lax;     // good default for forms
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15); // idle timeout
});

// MVC + global anti-forgery auto-validation on unsafe HTTP verbs
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

// Authorization policies (roles + claims)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
    options.AddPolicy("UserOnly", p => p.RequireRole("User"));
    // CanEditTask: Admin OR User with claim "permission=CanEditTask"
    options.AddPolicy("CanEditTaskPolicy", p =>
        p.RequireAssertion(ctx =>
            ctx.User.IsInRole("Admin")
            || (ctx.User.IsInRole("User") && ctx.User.HasClaim("permission", "CanEditTask"))));
});

var app = builder.Build();

// Migrate & seed roles/users
using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;
    var db = sp.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await DbInitializer.SeedAsync(sp);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
