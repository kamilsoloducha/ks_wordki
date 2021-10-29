using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api.Configuration
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMiddleware> _logger;

        public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            _logger.LogInformation("Start handling {request} at {startTime}", context.Request.Path, DateTime.Now);
            stopWatch.Start();

            await _next(context);

            stopWatch.Stop();
            _logger.LogInformation("Finish handling {request} at {startTime}.Execution time: {executionTime}", context.Request.Path, DateTime.Now, stopWatch.ElapsedMilliseconds);
        }

    }
}