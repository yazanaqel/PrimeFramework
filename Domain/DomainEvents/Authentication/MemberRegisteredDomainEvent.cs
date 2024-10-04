namespace Domain.DomainEvents.Authentication;
public sealed record userRegisteredDomainEvent(Guid Id, Guid userId) : DomainEvent(Id);
