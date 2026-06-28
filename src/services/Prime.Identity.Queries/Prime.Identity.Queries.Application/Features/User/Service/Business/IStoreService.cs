using Application.Pagination;
using CSharpFunctionalExtensions;
using Prime.Identity.Queries.Application.Features.Store.GetAllStores;
using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public interface IStoreService
{
    Task<Result<CursorPageResponse<GetAllStoresResponse>>> GetAllStoresAsync(GetAllStoresRequest request,CancellationToken ct = default);
    Task<Result<GetOwnerStoreResponse>> GetOwnerStoreAsync(CancellationToken ct = default);
}
