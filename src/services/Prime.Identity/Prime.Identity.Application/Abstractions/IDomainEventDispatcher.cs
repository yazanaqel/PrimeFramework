using Domain.Primitives;

namespace Application.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> events);
}

