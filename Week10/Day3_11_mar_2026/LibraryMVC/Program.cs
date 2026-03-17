using LibraryMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<Library1DbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Library1DB")));

            var app = builder.Build();

            // Configure HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Authors}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}