using Prime.Identity.Domain.Entities.Enums;
using Prime.Identity.Domain.Entities.Orders;

namespace Prime.Identity.Application.Features.Order.OrderItem;

public record ChangeOrderItemStatusRequest(string OrderItemId,OrderItemStatus OrderItemStatus);

