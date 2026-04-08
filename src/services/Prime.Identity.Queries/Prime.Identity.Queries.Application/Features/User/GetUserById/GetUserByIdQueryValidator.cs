using Application.Features.User.GetUserById;
using FluentValidation;

namespace Prime.Identity.Queries.Application.Features.User.GetUserById;

public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdRequest>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
