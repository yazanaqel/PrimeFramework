using FluentValidation;

namespace Application.Features.Members.LoginMember;
internal class LoginMemberCommandValidator : AbstractValidator<LoginMemberCommand>
{
    public LoginMemberCommandValidator()
    {
        RuleFor(x => x.Dto.Email).EmailAddress();
        RuleFor(x => x.Dto.Password).MaximumLength(10);
    }
}
