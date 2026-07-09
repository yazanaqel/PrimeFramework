namespace Prime.Identity.Queries.Application.Features.Product.GetStoreProducts;

public record GetStoreProductsResponse(
    Guid Id,
    Guid StoreId,
    Guid CategoryId,
    string Name,
    string Image,
    string Description,
    DateTime CreatedAt,
    DateTime? ModifiedAt);