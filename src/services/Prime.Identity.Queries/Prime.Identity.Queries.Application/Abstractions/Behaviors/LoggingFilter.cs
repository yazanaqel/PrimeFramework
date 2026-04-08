using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Diagnostics;

namespace Prime.Identity.Queries.Application.Abstractions.Behaviors;

public class LoggingFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        var stopwatch = Stopwatch.StartNew();

        // Extract request model (DTO)
        var requestModel = context.ActionArguments.Values.FirstOrDefault();

        Log.Information("➡️ Starting request {Action} with payload {@Payload}",
            actionName,
            requestModel);

        ActionExecutedContext executedContext;

        try
        {
            executedContext = await next();
        }
        catch(Exception ex)
        {
            stopwatch.Stop();

            Log.Error(ex,
                "❌ Exception in {Action} after {Elapsed} ms with payload {@Payload}",
                actionName,
                stopwatch.ElapsedMilliseconds,
                requestModel);

            throw;
        }

        stopwatch.Stop();

        var statusCode = executedContext.HttpContext.Response.StatusCode;

        Log.Information("⬅️ Completed {Action} with status {StatusCode} in {Elapsed} ms",
            actionName,
            statusCode,
            stopwatch.ElapsedMilliseconds);
    }
}
