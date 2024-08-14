using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CityTrivia.Infrastructure.Middleware
{
    public class PerformanceTrackerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceTrackerMiddleware> _logger;

        public PerformanceTrackerMiddleware(RequestDelegate next, ILogger<PerformanceTrackerMiddleware> logger) {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            var timeTracker = Stopwatch.StartNew();
            _logger.LogInformation($"Started execution of {context.Request.Path}");
            await _next.Invoke(context);
            timeTracker.Stop();
            _logger.LogInformation($"Completed execution of {context.Request.Path} in {timeTracker.Elapsed.TotalSeconds} seconds");
        }
    }
}
