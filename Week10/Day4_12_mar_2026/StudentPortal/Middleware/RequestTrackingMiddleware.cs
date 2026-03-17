namespace StudentPortal.Middleware
{
    using StudentPortal.Models;
    using StudentPortal.Services;
    using System.Diagnostics;

    public class RequestTrackingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRequestLogService logService)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            var log = new RequestLog
            {
                Url = context.Request.Path,
                ExecutionTime = stopwatch.ElapsedMilliseconds
            };

            logService.AddLog(log);
        }
    }
}
