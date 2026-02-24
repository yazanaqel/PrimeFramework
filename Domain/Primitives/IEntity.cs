namespace Domain.Primitives;

public interface IEntity
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

}
