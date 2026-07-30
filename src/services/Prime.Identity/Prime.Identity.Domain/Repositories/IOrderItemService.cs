using Prime.Identity.Domain.Entities.Enums;
using Prime.Identity.Domain.Entities.Orders;

namespace Prime.Identity.Domain.Repositories;

public interface IOrderItemService
{
    Task<bool> ChangeOrderItemStatus(List<ChangeOrderItemStatus> changeOrderItemStatus,CancellationToken ct);
}
public record ChangeOrderItemStatus(OrderItemId OrderItemId,OrderItemStatus OrderItemStatus);