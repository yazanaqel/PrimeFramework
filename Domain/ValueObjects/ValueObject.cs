namespace Domain.ValueObjects;
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(ValueObject? other)
    {
        return other is not null && ValuesAreEqual(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValuesAreEqual(other);
    }
    public static bool operator ==(ValueObject left,ValueObject right)
    {
        return Equals(left,right);
    }

    public static bool operator !=(ValueObject left,ValueObject right)
    {
        return !Equals(left,right);
    }
    public override int GetHashCode()
    {
        return GetAtomicValues().Aggregate(default(int),HashCode.Combine);
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}
