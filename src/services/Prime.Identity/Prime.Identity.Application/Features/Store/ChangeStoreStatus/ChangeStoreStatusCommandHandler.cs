using Application.Abstractions.Messaging;
using CSharpFunctionalExtensions;
using Prime.Identity.Application.Abstractions;
using Prime.Identity.Domain.Entities.Stores;
using Prime.Identity.Domain.Specifications.Business;

namespace Prime.Identity.Application.Features.Store.ChangeStoreStatus;

internal sealed class ChangeStoreStatusCommandHandler(
    IRepository<Domain.Entities.Stores.Store> repository) : ICommandHandler<ChangeStoreStatusCommand,bool>
{
    private readonly IRepository<Domain.Entities.Stores.Store> _repository = repository;

    public async Task<Result<bool>> Handle(ChangeStoreStatusCommand command,CancellationToken ct)
    {

        var storeId = StoreId.TryParse(command.Request.StoreId,out var parsedStoreId)
    ? parsedStoreId : throw new InvalidOperationException("Invalid store ID");

        var store = await _repository.FirstOrDefaultAsync(new GetOwnerStoreByStoreIdSpecification(storeId),ct);

        var storeUpdate = Domain.Entities.Stores.Store.Update(
            store.UserId,
            storeId,
            store.CategoryId,
            store.Name,
            store.Description,
            store.ImageCover,
            store.Image,
            store.Address,
            store.IsShippingAvailable,
            store.City,
            command.Request.StoreStatus);

            await _repository.UpdateAsync(storeUpdate,ct);

        return Result.Success(true);
    }

}