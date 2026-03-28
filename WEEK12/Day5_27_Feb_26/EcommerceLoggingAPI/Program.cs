using log4net;
using log4net.Config;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// ⭐ Load log4net config correctly
var repository =
    LogManager.GetRepository(
        Assembly.GetEntryAssembly());

var configFile =
    new FileInfo(
        Path.Combine(
            AppContext.BaseDirectory,
            "log4net.config"));

XmlConfigurator.Configure(
    repository,
    configFile);


// ⭐ Test logger (temporary debug)
var logger =
    LogManager.GetLogger(typeof(Program));

logger.Info("Application Started");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();