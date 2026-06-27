using FluentValidation;

namespace Prime.Identity.Application.Features.Category.Create;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Request.Name).NotEmpty().MaximumLength(20);
    }
}
