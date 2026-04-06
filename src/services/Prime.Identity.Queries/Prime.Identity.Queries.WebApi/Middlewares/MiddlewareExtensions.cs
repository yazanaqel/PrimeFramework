using Prime.Identity.Queries.WebApi.Middlewares.Exceptions;

namespace Prime.Identity.Queries.WebApi.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
}
