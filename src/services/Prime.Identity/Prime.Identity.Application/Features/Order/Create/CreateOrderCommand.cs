using Application.Abstractions.Messaging;

namespace Prime.Identity.Application.Features.Order.Create;

public record CreateOrderCommand(CreateOrderRequest Request,CancellationToken ct) : ICommand<bool>;
