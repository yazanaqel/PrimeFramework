namespace Domain.Primitives;

public abstract class Entity<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public TId Id { get; protected set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    public override bool Equals(object? obj)
    {
        if(obj is not Entity<TId> other)
            return false;

        if(ReferenceEquals(this,other))
            return true;

        if(EqualityComparer<TId>.Default.Equals(Id,default) ||
            EqualityComparer<TId>.Default.Equals(other.Id,default))
            return false;

        return EqualityComparer<TId>.Default.Equals(Id,other.Id);
    }

    public override int GetHashCode()
        => EqualityComparer<TId>.Default.GetHashCode(Id);
}


//public abstract class Entity : IEquatable<Entity>
//{
//    protected Entity(Guid id) => Id = id;

//    protected Entity() { }

//    public Guid Id { get; private init; }

//    public static bool operator ==(Entity? first,Entity? second) =>
//        first is not null && second is not null && first.Equals(second);

//    public static bool operator !=(Entity? first,Entity? second) =>
//        !(first == second);

//    public bool Equals(Entity? other)
//    {
//        if(other is null)
//        {
//            return false;
//        }

//        if(other.GetType() != GetType())
//        {
//            return false;
//        }

//        return other.Id == Id;
//    }

//    public override bool Equals(object? obj)
//    {
//        if(obj is null)
//        {
//            return false;
//        }

//        if(obj.GetType() != GetType())
//        {
//            return false;
//        }

//        if(obj is not Entity entity)
//        {
//            return false;
//        }

//        return entity.Id == Id;
//    }

//    public override int GetHashCode() => Id.GetHashCode() * 41;


//    private readonly List<IDomainEvent> _domainEvents = new();

//    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

//    protected void AddDomainEvent(IDomainEvent domainEvent)
//        => _domainEvents.Add(domainEvent);
//    public void ClearDomainEvents() => _domainEvents.Clear();

//}