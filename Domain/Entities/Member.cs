using Domain.DomainEvents;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Member : AggregateRoot, IAuditableEntity
{
    private Member() { }
    private Member(Guid id,Email email,string firstName,string lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public Email Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;

    public DateTime? ModifiedOnUtc { get; set; }

    public static Member Create(Guid id,Email email,string firstName,string lastName)
    {
        var member = new Member(id,email,firstName,lastName);

        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(Guid.NewGuid(),member.Id));

        return member;
    }

}
