namespace Prime.Identity.Application.Abstractions.Auth;

public interface IRequireUser
{
    string? UserId { get; set; }
}
