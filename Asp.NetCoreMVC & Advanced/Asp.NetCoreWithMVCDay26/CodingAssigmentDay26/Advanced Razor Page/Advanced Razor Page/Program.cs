
using AdvancedRazorPages;

var builder = WebApplication.CreateBuilder(args);

// Keep Startup pattern (assignment asks for Startup.cs)
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);

app.Run();
