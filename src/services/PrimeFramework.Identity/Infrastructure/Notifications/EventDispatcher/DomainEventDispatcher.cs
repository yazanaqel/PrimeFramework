using Application.Abstractions;
using Domain.Primitives;
using MediatR;

namespace Infrastructure.Notifications.EventDispatcher;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> events)
    {
        foreach(var domainEvent in events)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
