using Ardalis.Specification.EntityFrameworkCore;
using Domain.Abstractions;

namespace Infrastructure.Abstractions;

public class ReadRepository<T> : RepositoryBase<T>, IReadRepository<T>
    where T : class
{
    public ReadRepository(ReadOnlyDbContext dbContext) : base(dbContext)
    {
    }

}