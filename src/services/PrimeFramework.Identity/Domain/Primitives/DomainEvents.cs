namespace Domain.Primitives;
public abstract record DomainEvent(Guid EventId) : IDomainEvent;
