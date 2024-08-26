using FluentValidation;

namespace Application.Features.Members.CreateMember;
internal class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(x => x.Dto.Email).EmailAddress();

        RuleFor(x => x.Dto.FirstName).NotEmpty();

        RuleFor(x => x.Dto.LastName).NotEmpty();
    }
}
