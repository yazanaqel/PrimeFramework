using Prime.Identity.Queries.Domain.Entities.Enums;

namespace Prime.Identity.Queries.Application.Features.Order;

public record OrderItemResponse(string OrderItemId,string ProductName,OrderItemStatus OrderItemStatus);
