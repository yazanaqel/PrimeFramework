﻿using Domain.Shared;

namespace Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 255;

    private Email(string value) => Value = value;
    private Email() { }

    public string Value { get; private set; }

    //public static Result<Email> Create(string email) =>
    //    Result.Create(email)
    //        .Ensure(
    //            e => !string.IsNullOrWhiteSpace(e),
    //            DomainErrors.Email.Empty)
    //        .Ensure(
    //            e => e.Length <= MaxLength,
    //            DomainErrors.Email.TooLong)
    //        .Ensure(
    //            e => e.Split('@').Length == 2,
    //            DomainErrors.Email.InvalidFormat)
    //        .Map(e => new Email(e));

    public static Result<Email> CreateEmail(string email) =>
        Result.Success(new Email(email));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
