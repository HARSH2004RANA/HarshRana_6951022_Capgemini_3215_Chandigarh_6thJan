var builder = WebApplication.CreateBuilder(args);

// 🔹 1. Add Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        p => p.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});
// 🔹 2. Build app (IMPORTANT)
var app = builder.Build();
app.UseCors("AllowAll");
// 🔹 3. Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 🔹 4. Map Razor Pages
app.MapRazorPages();

app.Run();