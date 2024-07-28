using Domain.Base;

namespace Domain.Interfaces;

public interface IAsyncRepository<T> where T : BaseEntity
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<T> GetByIdAsync(long id);
    Task<List<T>> GetAsync();
}