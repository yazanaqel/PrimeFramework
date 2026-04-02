using Domain.Exceptions;

namespace Application.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string message,Guid userId) : base(message) { }
}
