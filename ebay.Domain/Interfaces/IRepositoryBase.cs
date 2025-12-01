using ebay.Domain.Entities;

namespace ebay.Domain.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    // crud chung chung cơ bản
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();

    // lấy ra x T bản ghi
    Task<IEnumerable<T>> GetNAsync(int n);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}