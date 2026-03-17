using ProductManagement.Filters;

namespace ProductModel
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container and register global filters
			builder.Services.AddControllersWithViews(options =>
			{
				options.Filters.Add<CustomExceptionFilter>();
			});

			builder.Services.AddScoped<LogActionFilter>(); // Logging filter

			var app = builder.Build();

			// Configure the HTTP request pipeline.
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
				pattern: "{controller=Product}/{action=Index}/{id?}")
				.WithStaticAssets();

			app.Run();
		}
	}
}