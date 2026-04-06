using Application.Features.User.GetAllUsers;
using FluentValidation;

namespace Prime.Identity.Queries.Application.Features.User.GetAllUsers;

public sealed class GetAllUsersQueryValidation: AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidation()
    {
        RuleFor(x => x.Request.Size)
            .InclusiveBetween(10,50)
            .WithMessage("Size must be between 10 and 50.");

        RuleFor(x => x.Request.Search)
            .Length(3,50)
            .When(x => x.Request.Search is not null)
            .WithMessage("Search term must be between 3 and 50 characters.");
    }
}
