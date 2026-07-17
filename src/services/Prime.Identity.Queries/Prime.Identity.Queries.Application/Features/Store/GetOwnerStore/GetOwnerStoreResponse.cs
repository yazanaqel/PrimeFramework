using Prime.Identity.Queries.Domain.Entities.Enums;

namespace Prime.Identity.Queries.Application.Features.Store.GetOwnerStore;

public record GetOwnerStoreResponse(
    Guid StoreId,
    Guid UserId,
    Guid CategoryId,
    string Name,
    string ImageCover,
    string Image,
    string Description,
    string Address,
    bool IsShippingAvailable,
    City City,
    StoreStatus StoreStatus,
    DateTime CreatedAt,
    DateTime? ModifiedAt);
