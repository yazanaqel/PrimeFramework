using Application.Features.User.GetAllUsers;
using FluentValidation;

namespace Prime.Identity.Queries.Application.Features.User.GetAllUsers;

public sealed class GetAllUsersQueryValidation: AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidation()
    {
        RuleFor(x => x.Request.Size)
            .GreaterThanOrEqualTo(10)
            .LessThan(50)
            .WithMessage("Size must be between 10 and 50.");
    }
}
