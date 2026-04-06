using Application.Features.User.LogoutUser;
using FluentValidation;

namespace Prime.Identity.Application.Features.User.LogoutUser;

public sealed class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommand>
{
    public LogoutUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}
