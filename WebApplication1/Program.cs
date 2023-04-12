using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        // Add session support
                        services.AddDistributedMemoryCache();
                        services.AddSession(options =>
                        {
                            options.Cookie.HttpOnly = true;
                            options.Cookie.IsEssential = true;
                        });
                        services.AddControllersWithViews();
                        services.AddRazorPages();
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseSession(); // Enable session middleware
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                            endpoints.MapRazorPages();
                        });
                    });
                });
    }
}