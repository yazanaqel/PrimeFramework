using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception,"Unhandled exception occurred");

        var (status,title) = exception switch
        {
            ValidationException => (StatusCodes.Status422UnprocessableEntity,"Validation error"),
            NotFoundException => (StatusCodes.Status404NotFound,"Resource not found"),
            DomainException => (StatusCodes.Status400BadRequest,"Domain rule violated"),
            _ => (StatusCodes.Status500InternalServerError,"Server error")
        };

        var problem = new ProblemDetails
        {
            Title = title,
            Detail = exception.Message,
            Status = status,
            Instance = context.TraceIdentifier
        };

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem,cancellationToken);

        return true;
    }
}
