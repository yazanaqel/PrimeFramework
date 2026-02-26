using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public readonly record struct PhoneNumber
{
    private static readonly Regex PhoneRegex =
        new(@"^\+?[0-9]{7,15}$",RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty");

        value = value.Trim();

        if(!PhoneRegex.IsMatch(value))
            throw new ArgumentException("Invalid phone number format");

        Value = value;
    }

    public override string ToString() => Value;
}
