using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add JWT Authentication to Swagger

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description =
                "Enter 'Bearer' [space] and then your token.\n\nExample: Bearer abc123"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                new string[] {}
            }
        });

});


// JWT SETTINGS

var key =
Encoding.ASCII.GetBytes(
    "THIS_IS_MY_SUPER_SECRET_KEY_12345");

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;

    options.SaveToken = true;

    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey =
                new SymmetricSecurityKey(key),

            ValidateIssuer = false,
            ValidateAudience = false
        };

    // JWT Logging Events

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var log =
                LogManager.GetLogger("Middleware");

            log.Error(
                "Authentication Failed",
                context.Exception);

            return Task.CompletedTask;
        },

        OnChallenge = context =>
        {
            var log =
                LogManager.GetLogger("Middleware");

            log.Warn(
                $"Unauthorized access to {context.Request.Path}");

            return Task.CompletedTask;
        },

        OnTokenValidated = context =>
        {
            var log =
                LogManager.GetLogger("Middleware");

            log.Info(
                "Valid token used");

            return Task.CompletedTask;
        }
    };

});

builder.Services.AddAuthorization();

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


// Request Logging Middleware

app.Use(async (context, next) =>
{
    var log =
        LogManager.GetLogger("Request");

    log.Info(
        $"{context.Request.Method} {context.Request.Path}");

    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();