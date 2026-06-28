using Prime.Identity.Domain.Entities.Categories;
using Prime.Identity.Domain.Entities.Enums;

namespace Prime.Identity.Application.Features.Store.Create;

public record CreateStoreRequest(
    string Name,
    string Description,
    string ImageCover,
    string Image,
    string Address,
    bool IsShippingAvailable,
    City City,
    CategoryId CategoryId);
