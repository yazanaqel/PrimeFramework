namespace Domain.ValueObjects;

public abstract class ValueObject<T> : IEquatable<T>
    where T : ValueObject<T>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(T? other)
    {
        if(other is null)
            return false;

        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    public override bool Equals(object? obj) =>
        obj is T valueObject && Equals(valueObject);

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1,(current,obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }
}

