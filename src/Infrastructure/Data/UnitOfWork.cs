using Domain.Interfaces;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _dbContext;

    public UnitOfWork(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IAsyncRepository<T> AsyncRepository<T>() where T : Domain.Base.BaseEntity
    {
        return new RepositoryBase<T>(_dbContext);
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}