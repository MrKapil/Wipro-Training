using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Middleware
{
    // Middleware to log incoming requests and outgoing responses
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            // Log incoming request
            _logger.LogInformation("--> {Method} {Path}", context.Request.Method, context.Request.Path);

            await _next(context); // Call the next middleware in the pipeline

            sw.Stop();

            // Log outgoing response
            _logger.LogInformation("<-- {StatusCode} {Path} ({Elapsed} ms)",
                context.Response.StatusCode,
                context.Request.Path,
                sw.ElapsedMilliseconds);
        }
    }
}
