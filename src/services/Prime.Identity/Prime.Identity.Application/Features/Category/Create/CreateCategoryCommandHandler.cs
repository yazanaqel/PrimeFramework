using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Prime.Identity.Application.Abstractions;

namespace Prime.Identity.Application.Features.Category.Create;

internal sealed class CreateCategoryCommandHandler(IRepository<Domain.Entities.Categories.Category> repository) : ICommandHandler<CreateCategoryCommand,bool>
{
    private readonly IRepository<Domain.Entities.Categories.Category> _repository = repository;

    public async Task<Result<bool>> Handle(CreateCategoryCommand command,CancellationToken ct)
    {
        var category = Domain.Entities.Categories.Category.Create(command.Request.Name,command.Request.Description,command.Request.ParentCategoryId);

        await _repository.AddAsync(category,ct);

        return Result.Success(true);
    }


}