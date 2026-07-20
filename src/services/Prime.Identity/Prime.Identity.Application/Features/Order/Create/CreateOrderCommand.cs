using Application.Abstractions.Messaging;

namespace Prime.Identity.Application.Features.Order.Create;

public record CreateOrderCommand(List<CreateOrderRequest> Request,CancellationToken ct) : ICommand<bool>;
