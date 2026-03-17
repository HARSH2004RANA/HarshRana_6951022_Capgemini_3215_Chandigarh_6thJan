using Microsoft.EntityFrameworkCore;
using Model_level_Validation.Models;

namespace Model_level_Validation
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add MVC
			builder.Services.AddControllersWithViews();

			// Add DbContext
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					builder.Configuration.GetConnectionString("DefaultConnection")));

			var app = builder.Build();

			// Configure middleware
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Companies}/{action=Index}/{id?}");

			app.Run();
		}
	}
}