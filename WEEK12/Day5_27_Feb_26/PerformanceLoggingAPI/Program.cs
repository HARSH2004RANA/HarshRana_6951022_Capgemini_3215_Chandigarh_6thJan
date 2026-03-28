using log4net;
using log4net.Config;
using PerformanceLoggingAPI.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Load log4net

var repository =
    LogManager.GetRepository(
        Assembly.GetEntryAssembly());

XmlConfigurator.Configure(
    repository,
    new FileInfo(
        Path.Combine(
            AppContext.BaseDirectory,
            "log4net.config")));


// Use Performance Middleware

app.UseMiddleware<PerformanceMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();