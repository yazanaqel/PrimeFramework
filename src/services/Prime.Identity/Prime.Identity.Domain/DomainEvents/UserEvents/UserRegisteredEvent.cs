using Domain.Primitives;

namespace Domain.Entities.Users;

public sealed record UserRegisteredEvent(
    Guid EventId,
    Guid UserId,
    string Email,
    DateTime OccurredOn) : DomainEvent(EventId);