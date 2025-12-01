using ebay.Infrastructure.Data;
using ebay.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ebay.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly EBayDbContext _db;
    private readonly DbSet<T> _dbSet;
    public RepositoryBase(EBayDbContext db)
    {
        _db = db;
        _dbSet = db.Set<T>();
    }
    public async Task<T> AddAsync(T entity)
    {
       await _dbSet.AddAsync(entity);
       return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // tìm entity theo id
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            // vì T có thể là bất kỳ class nào nên ta dùng reflection để set Deleted = true
            entity.GetType().GetProperty("Deleted")?.SetValue(entity, true);
            _dbSet.Update(entity);
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.Take(20).ToListAsync();;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetNAsync(int n)
    {
        return await _dbSet.Take(n).ToListAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return entity;
    }
}