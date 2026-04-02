namespace Domain.ValueObjects;

using System.Text.RegularExpressions;

public readonly record struct Email
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; }

    public Email(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty",nameof(value));


        value = value.Trim().ToLowerInvariant();

        if(!EmailRegex.IsMatch(value))
            throw new ArgumentException("Invalid email format",nameof(value));

        Value = value;
    }


    public bool Equals(Email other)
        => string.Equals(Value,other.Value,StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode()
        => StringComparer.OrdinalIgnoreCase.GetHashCode(Value);

    public override string ToString() => Value;


    public static bool TryCreate(string value,out Email email)
    {
        try
        {
            email = new Email(value);
            return true;
        }
        catch
        {
            email = default;
            return false;
        }
    }

    public static Email Create(string value)
    {
        if(TryCreate(value,out Email email))
        {
            return email;
        }
        return default;
    }
}

