using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Prime.Identity.Queries.Application.Abstractions.Auth;
using Prime.Identity.Queries.Application.Features.Store.GetAllStores;
using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;
using Prime.Identity.Queries.Domain.Specifications.Business;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public class StoreService(IReadRepository<Domain.Entities.Business.Store> storeRepository,ICurrentUserService currentUserService) : IStoreService
{
    private readonly IReadRepository<Domain.Entities.Business.Store> _storeRepository = storeRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private const string baseUrl = "https://localhost:7104/";
    public async Task<Result<IEnumerable<GetAllStoresResponse>>> GetAllStores(GetAllStoresRequest request,CancellationToken ct = default)
    {

        var spec = new GetAllStoresSpec(request.Search);

        var stores = await _storeRepository.ListAsync(spec,ct);

        return Result.Success(stores.Select(s => new GetAllStoresResponse(
            s.Id,
            s.UserId,
            s.CategoryId,
            s.Name,
            $"{baseUrl}{s.ImageCover}",
            $"{baseUrl}{s.Image}",
            s.Description,
            s.Address,
            s.IsShippingAvailable,
            s.City,
            s.StoreStatus,
            s.CreatedAt,
            s.ModifiedAt
        )));
    }


    public async Task<Result<GetOwnerStoreResponse>> GetOwnerStoreAsync(CancellationToken ct = default)
    {
        var userId = Guid.TryParse(_currentUserService.UserId,out var parsedUserId)
? parsedUserId : throw new InvalidOperationException("Invalid user ID");

        var spec = new GetOwnerStoreByIdSpecification(parsedUserId);

        var store = await _storeRepository.FirstOrDefaultAsync(spec,ct);

        if(store is null)
            return Result.Failure<GetOwnerStoreResponse>("Store not found for the current user.");

        var response = new GetOwnerStoreResponse(
            store.Id,
            store.UserId,
            store.CategoryId,
            store.Name,
            $"{baseUrl}{store.ImageCover}",
            $"{baseUrl}{store.Image}",
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

    public async Task<Result<GetOwnerStoreResponse>> GetStoreById(Guid storeId,CancellationToken ct = default)
    {

        var storeIdParsed = Guid.TryParse(storeId.ToString(), out var parsedStoreId)
? parsedStoreId : throw new InvalidOperationException("Invalid Store ID");
        
        var spec = new GetStoreByIdSpecification(storeIdParsed);

        var store = await _storeRepository.FirstOrDefaultAsync(spec,ct);

        if(store is null)
            return Result.Failure<GetOwnerStoreResponse>("Store not found for the current user.");

        var response = new GetOwnerStoreResponse(
            store.Id,
            store.UserId,
            store.CategoryId,
            store.Name,
            $"{baseUrl}{store.ImageCover}",
            $"{baseUrl}{store.Image}",
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
