using System.Net;
using System.Text.Json;
using EventBooking.API.Helpers;

namespace EventBooking.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            var response =
                new ApiResponse(
                    context.Response.StatusCode,
                    "An unexpected error occurred",
                    ex.Message);

            var json =
                JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}