namespace Domain.Interfaces.Repositories.Base;

public interface IAsyncRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<T> GetByIdAsync(long id);
    Task<List<T>> GetAsync();
}