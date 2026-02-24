using FluentValidation;

namespace Application.Features.Authentication.LoginUser;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Request.Email).EmailAddress();
        RuleFor(x => x.Request.Password).MinimumLength(6);
    }
}
