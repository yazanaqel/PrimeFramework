using FluentValidation;

namespace Application.Features.Authentication.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Request.Email).EmailAddress();

        RuleFor(x => x.Request.Role)
            .MaximumLength(10)
            .When(x => !string.IsNullOrWhiteSpace(x.Request.Role));

        RuleFor(x => x.Request.Password).MinimumLength(6);

        RuleFor(x => x.Request.ConfirmPassword)
            .Equal(x => x.Request.Password)
            .WithMessage("The password and confirmation password do not match.");

        RuleFor(x => x.Request.PhoneNumber)
            .NotEmpty()
            .Length(10)
            .Must(x => x.All(char.IsDigit))
            .Must(x => x.StartsWith("09"))
            .WithMessage("Phone number must be 10 digits and start with 09.");


    }
}
