namespace Domain.ValueObjects;

using System.Text.RegularExpressions;


public sealed class Email : ValueObject<Email>
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",RegexOptions.Compiled);

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.",nameof(email));

        email = email.Trim();

        //if(!EmailRegex.IsMatch(email))
        //    throw new ArgumentException("Invalid email format.",nameof(email));

        return new Email(email);
    }

    public override string ToString() => Value;

    #region Equality


    public static bool operator ==(Email? left,Email? right) => Equals(left,right);

    public static bool operator !=(Email? left,Email? right) =>!Equals(left,right);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }



    #endregion
}

