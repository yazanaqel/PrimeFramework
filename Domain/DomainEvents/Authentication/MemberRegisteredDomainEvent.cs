namespace Domain.DomainEvents.Authentication;
public sealed record MemberRegisteredDomainEvent(Guid Id, Guid MemberId) : DomainEvent(Id);
