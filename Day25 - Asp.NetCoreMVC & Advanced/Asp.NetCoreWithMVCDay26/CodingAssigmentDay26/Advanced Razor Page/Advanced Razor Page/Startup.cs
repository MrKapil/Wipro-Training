
using AdvancedRazorPages.Services;

namespace AdvancedRazorPages
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                
                options.Conventions.AddPageRoute("/Products/Details", "Products/{id:int}");
                options.Conventions.AddPageRoute("/Categories/Index", "Categories/{name}");

            });

            // In-memory storage for products
            services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
