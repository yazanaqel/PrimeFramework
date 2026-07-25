using Prime.Identity.Queries.Application.Features.Category;
using Prime.Identity.Queries.Application.Features.Product.GetStoreProducts;
using Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;

namespace Prime.Identity.Queries.Application.Features.Product;

public record GetProductByIdResponse(
    Guid ProductId,
    string Name,
    string Image,
    string Description,
    decimal Price,
    DateTime CreatedAt,
    DateTime? ModifiedAt,
    GetOwnerStoreResponse Store,
    List<GetStoreProductsResponse>? SimilarProducts);