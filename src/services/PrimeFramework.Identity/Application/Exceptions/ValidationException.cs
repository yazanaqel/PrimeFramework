using Domain.Exceptions;

namespace Application.Exceptions;

public class ValidationException : DomainException
{
    public IReadOnlyList<string> Errors { get; }
    public ValidationException(IEnumerable<string> errors) : base("Validation failed")
    {
        Errors = errors.ToList();
    }
}