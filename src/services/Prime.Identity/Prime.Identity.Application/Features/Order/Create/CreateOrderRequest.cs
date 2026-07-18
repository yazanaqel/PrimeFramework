using Prime.Identity.Domain.Entities.Products;

namespace Prime.Identity.Application.Features.Order.Create;

public record CreateOrderRequest(List<ProductId> ProductIds,int Quantity);
