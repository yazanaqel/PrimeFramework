using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Prime.Identity.Application.Abstractions;
using Prime.Identity.Application.Abstractions.Auth;
using Prime.Identity.Domain.Entities.Users;

namespace Prime.Identity.Application.Features.Store.Create;

internal sealed class CreateStoreCommandHandler(IRepository<Domain.Entities.Stores.Store> repository,ICurrentUserService currentUserService) : ICommandHandler<CreateStoreCommand,bool>
{
    private readonly IRepository<Domain.Entities.Stores.Store> _repository = repository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<Result<bool>> Handle(CreateStoreCommand command,CancellationToken ct)
    {

        var userId = UserId.TryParse(_currentUserService.UserId,out var parsedUserId)
    ? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var store = Domain.Entities.Stores.Store.Create(
            userId,
            command.Request.CategoryId,
            command.Request.Name,
            command.Request.Description,
            command.Request.ImageCover,
            command.Request.Image,
            command.Request.Address,
            command.Request.IsShippingAvailable,
            command.Request.City);

        var result = await _repository.AddAsync(store,ct);

        return Result.Success(true);
    }

}