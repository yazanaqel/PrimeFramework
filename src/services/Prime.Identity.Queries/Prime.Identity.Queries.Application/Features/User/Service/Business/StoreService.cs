using Application.Features.User.GetUserById;
using Application.Pagination;
using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Specifications.User;
using Prime.Identity.Queries.Application.Abstractions.Auth;
using Prime.Identity.Queries.Application.Abstractions.Cache;
using Prime.Identity.Queries.Application.Features.Store.GetAllStores;
using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;
using Prime.Identity.Queries.Domain.Specifications.Business;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public class StoreService(IReadRepository<Domain.Entities.Business.Store> storeRepository,ICurrentUserService currentUserService) : IStoreService
{
    private readonly IReadRepository<Domain.Entities.Business.Store> _storeRepository = storeRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public Task<Result<CursorPageResponse<GetAllStoresResponse>>> GetAllStoresAsync(GetAllStoresRequest request,CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<GetOwnerStoreResponse>> GetOwnerStoreAsync(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var spec = new GetOwnerStoreByIdSpecification(parsedUserId);

        var store = await _storeRepository.FirstOrDefaultAsync(spec,ct);

        if(store is null)
            throw new Exception($"Store With User Id : {parsedUserId} Not Found!");

        var response = new GetOwnerStoreResponse(
            store.Id,
            store.UserId,
            store.CategoryId,
            store.Name,
            store.ImageCover,
            store.Image,
            store.Description,
            store.Address,
            store.IsShippingAvailable,
            store.City,
            store.StoreStatus,
            store.CreatedAt,
            store.ModifiedAt
        );

        return Result.Success(response);
    }

}
