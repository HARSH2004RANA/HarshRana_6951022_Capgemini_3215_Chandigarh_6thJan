using log4net;

namespace PerformanceLoggingAPI.Middleware
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly ILog log =
            LogManager.GetLogger(typeof(PerformanceMiddleware));

        public PerformanceMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context)
        {
            var startTime = DateTime.Now;

            log.Info(
                $"API Started: {context.Request.Method} {context.Request.Path}");

            try
            {
                await _next(context);

                var duration =
                    DateTime.Now - startTime;

                var seconds =
                    duration.TotalSeconds;

                if (seconds > 3)
                {
                    log.Warn(
                        $"Slow API detected: {seconds:F2} sec - {context.Request.Path}");
                }
                else
                {
                    log.Info(
                        $"API Completed in {seconds:F2} sec - {context.Request.Path}");
                }
            }
            catch (Exception ex)
            {
                log.Error(
                    $"API failed: {context.Request.Path}",
                    ex);

                throw;
            }
        }
    }
}