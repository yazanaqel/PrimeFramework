using FluentValidation;

namespace Application.Features.Authentication.RegisterUser;

internal class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");
    }
}
