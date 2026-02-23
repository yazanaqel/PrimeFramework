using Domain.Exceptions;

namespace Application.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message) { }
}
