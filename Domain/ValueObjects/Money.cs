namespace Domain.ValueObjects;

public readonly record struct Money
{
    public string Value { get; }

    public Money(decimal Amount,string Currency)
    {
        if(Amount < 0)
            throw new ArgumentException("Amount cannot be negative");

        if(string.IsNullOrWhiteSpace(Currency))
            throw new ArgumentException("Currency cannot be empty");

        Value = Currency.ToUpperInvariant() + Amount.ToString();

    }

    public override string ToString() => Value;
}

