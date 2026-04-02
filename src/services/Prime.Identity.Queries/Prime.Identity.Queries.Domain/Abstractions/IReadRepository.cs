using Ardalis.Specification;

namespace Domain.Abstractions;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}