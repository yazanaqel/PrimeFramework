using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Prime.Identity.Queries.WebApi.Middlewares.Exceptions;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context,RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var problem = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed"
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch(Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Detail = ex.Message
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
