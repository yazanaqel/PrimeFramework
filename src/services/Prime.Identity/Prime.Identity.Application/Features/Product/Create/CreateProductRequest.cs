using Microsoft.AspNetCore.Http;
using Prime.Identity.Domain.Entities.Categories;

namespace Prime.Identity.Application.Features.Product.Create;

public record CreateProductRequest(
    string Name,
    string Description,
    IFormFile ImageFile,
    int StockQuantity,
    decimal UnitPrice,
    CategoryId CategoryId);