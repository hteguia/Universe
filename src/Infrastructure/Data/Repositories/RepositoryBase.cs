using System.Linq.Expressions;
using Domain.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(DatabaseContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
    }
    
    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.FromResult(true);
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public Task<List<T>> GetAsync()
    {
        return _dbSet.ToListAsync();
    }
}