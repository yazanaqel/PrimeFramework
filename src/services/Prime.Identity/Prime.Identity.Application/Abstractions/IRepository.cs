using Ardalis.Specification;

namespace Prime.Identity.Application.Abstractions;

public interface IRepository<T> : IRepositoryBase<T>
    where T : class
{
}