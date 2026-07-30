using Application.Abstractions.Messaging;

namespace Prime.Identity.Application.Features.Order.OrderItem;

public record ChangeOrderItemStatusCommand(List<ChangeOrderItemStatusRequest> Request,CancellationToken ct) : ICommand<bool>;
