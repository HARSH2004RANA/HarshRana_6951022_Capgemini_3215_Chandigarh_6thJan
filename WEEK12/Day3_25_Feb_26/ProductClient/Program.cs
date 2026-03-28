namespace ProductClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services
            builder.Services.AddRazorPages();

            builder.Services.AddHttpClient("ProductApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5006/api/");
            });

            // ✅ Health Check Service
            builder.Services.AddHealthChecks();

            var app = builder.Build();

            // Pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages().WithStaticAssets();

            // ✅ Health Endpoint
            app.MapHealthChecks("/health");

            app.Run();
        }
    }
}