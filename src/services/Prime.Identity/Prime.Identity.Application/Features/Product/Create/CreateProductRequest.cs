using Prime.Identity.Domain.Entities.Categories;

namespace Prime.Identity.Application.Features.Product.Create;

public record CreateProductRequest(
    string Name,
    string Description,
    string Image,
    int StockQuantity,
    decimal UnitPrice,
    CategoryId CategoryId);