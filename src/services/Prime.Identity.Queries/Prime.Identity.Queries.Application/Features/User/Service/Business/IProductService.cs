using CSharpFunctionalExtensions;
using Prime.Identity.Queries.Application.Features.Product.GetStoreProducts;

namespace Prime.Identity.Queries.Application.Features.User.Service.Business;

public interface IProductService
{
    Task<Result<List<GetStoreProductsResponse>>> GetStoreProductsAsync(CancellationToken ct = default);
    Task<Result<List<GetStoreProductsResponse>>> GetStoreProductsById(Guid storeId, CancellationToken ct = default);
}
