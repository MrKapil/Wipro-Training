using EcommerceApp.Filters;
using EcommerceApp.Services;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// MVC + global action filter
builder.Services.AddControllersWithViews(o =>
{
    o.Filters.Add<LogActionFilter>();   // our custom filter
});

// Razor Pages (we'll show Bill on a Razor Page)
builder.Services.AddRazorPages();

// Sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
    o.IdleTimeout = TimeSpan.FromMinutes(30);
});

// DI for app services
builder.Services.AddSingleton<ProductService>();
builder.Services.AddScoped<CartService>();

// Keep JSON compact (for TempData serialization)
builder.Services.Configure<JsonOptions>(opt => { opt.SerializerOptions.WriteIndented = false; });

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();
// Custom & default routes
app.MapControllerRoute(
    name: "products",
    pattern: "products",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "billing",
    pattern: "order/bill",
    defaults: new { page = "/Billing/Index" }); // Razor Page

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapRazorPages();

app.Run();
