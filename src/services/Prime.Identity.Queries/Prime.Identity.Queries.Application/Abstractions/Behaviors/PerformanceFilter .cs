namespace Prime.Identity.Queries.Application.Abstractions.Behaviors;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

public class PerformanceFilter : IAsyncActionFilter
{
    private readonly ILogger<PerformanceFilter> _logger;
    private readonly int _thresholdMs;

    public PerformanceFilter(ILogger<PerformanceFilter> logger,int thresholdMs = 500)
    {
        _logger = logger;
        _thresholdMs = thresholdMs;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        var executedContext = await next();

        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;

        if(elapsed > _thresholdMs)
        {
            var actionName = context.ActionDescriptor.DisplayName;

            _logger.LogWarning(
                "Slow request detected: {Action} took {Elapsed} ms",
                actionName,
                elapsed
            );
        }

        // Optional: Add header
        executedContext.HttpContext.Response.Headers["X-Elapsed-Time-ms"] = elapsed.ToString();
    }
}
