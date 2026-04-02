using FluentValidation;

namespace Application.Features.Authentication.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Request.Email).EmailAddress();
        RuleFor(x => x.Request.Password).MinimumLength(6);
        RuleFor(x => x.Request.ConfirmPassword).Equal(x => x.Request.Password)
            .WithMessage("The password and confirmation password do not match.");
    }
}
