using Microsoft.AspNetCore.Http;
using Prime.Identity.Application.Abstractions.Auth;

namespace Prime.Identity.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _http;
    public CurrentUserService(IHttpContextAccessor http) => _http = http;
    public string? UserId => _http.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                             ?? _http.HttpContext?.User?.FindFirst("sub")?.Value;
    public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);
}