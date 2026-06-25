using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public readonly record struct PhoneNumber
{
    private static readonly Regex MobileRegex =
        new(@"^09\d{8}$",RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Mobile number cannot be empty",nameof(value));

        value = value.Trim();

        if(!MobileRegex.IsMatch(value))
            throw new ArgumentException("Invalid Syrian mobile number format",nameof(value));

        Value = value;
    }

    public bool Equals(PhoneNumber other)
        => string.Equals(Value,other.Value,StringComparison.Ordinal);

    public override int GetHashCode()
        => StringComparer.Ordinal.GetHashCode(Value);

    public override string ToString() => Value;

    public static bool TryCreate(string value,out PhoneNumber mobile)
    {
        try
        {
            mobile = new PhoneNumber(value);
            return true;
        }
        catch
        {
            mobile = default;
            return false;
        }
    }

    public static PhoneNumber Create(string value)
    {
        if(TryCreate(value,out PhoneNumber mobile))
            return mobile;

        return default;
    }
}
