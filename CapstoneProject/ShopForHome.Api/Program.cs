using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopForHome.Api.Data;
using ShopForHome.Api.Helpers;
using ShopForHome.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<JwtTokenGenerator>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<WishlistService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CouponService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<UserService>();

// Swagger with Bearer auth
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopForHome API", Version = "v1" });

    // Define Bearer Auth
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey, // <-- IMPORTANT
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT.\nExample: Bearer eyJhbGciOiJIUzI1..."
    });

    // Require Bearer token globally
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


// CORS for Angular dev
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", p => p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
});

// JWT
var key = builder.Configuration["Jwt:Key"] ?? "ReplaceWithRealKey1234567890";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// helpers
builder.Services.AddSingleton<JwtTokenGenerator>();

var app = builder.Build();

// middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("DevCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Seed DB
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(ctx);
}

app.Run();
