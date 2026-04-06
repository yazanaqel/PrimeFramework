using Domain.Primitives;
using Prime.Identity.Domain.Entities.Users;

namespace Domain.Entities.Users;

public sealed record UserRegisteredEvent(
    Guid EventId,
    UserId UserId,
    string Email,
    DateTime OccurredOn) : DomainEvent(EventId);