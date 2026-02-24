using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

internal class GenericRepository<T>(ApplicationDbContext applicationDbContext) : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

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
