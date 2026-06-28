namespace Prime.Identity.Application.Abstractions.Auth;

public interface ICurrentUserService
{
    string? UserId { get; }
    bool IsAuthenticated { get; }
}
