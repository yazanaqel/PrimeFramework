using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure;
using Prime.Identity.Application.Abstractions;

namespace Prime.Identity.Infrastructure.Abstractions;

public class Repository<T> : RepositoryBase<T>, IRepository<T>
    where T : class
{
    public Repository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

}