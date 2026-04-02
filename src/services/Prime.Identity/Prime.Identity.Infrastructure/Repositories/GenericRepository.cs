using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

internal abstract class GenericRepository<T, TId> : IGenericRepository<T> where T : Domain.Primitives.Entity<TId> /*where TId : class*/
{
    private readonly ApplicationDbContext _applicationDbContext;

    protected GenericRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _applicationDbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _applicationDbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T,bool>> predicate)
    {
        return await _applicationDbContext.Set<T>()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _applicationDbContext.Set<T>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _applicationDbContext.Set<T>().AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _applicationDbContext.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        _applicationDbContext.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _applicationDbContext.Set<T>().RemoveRange(entities);
    }
}
