using Domain.Primitives;

namespace Domain.Entities.Users;

public sealed record UserRegisteredEvent(
    Guid Id,
    Guid UserId,
    string Email,
    DateTime OccurredOn) : DomainEvent(Id);