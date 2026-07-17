using Microsoft.AspNetCore.Http;
using Prime.Identity.Domain.Entities.Categories;
using Prime.Identity.Domain.Entities.Enums;

namespace Prime.Identity.Application.Features.Store.Create;

public record CreateStoreRequest(
    string Name,
    string Description,
    string Address,
    CategoryId CategoryId,
    bool IsShippingAvailable,
    City City,
    IFormFile Image,
    IFormFile ImageCover);