namespace ebay.Application.Interfaces;

public interface IServiceBase<TEntity,TDto> 
where TEntity : class 
where TDto : class
{
    Task<List<TDto>> GetAllAsync();
    // get by id 
    Task<TDto?> GetByIdAsync(int id);

    // create
    Task<TDto> CreateAsync(TDto dto);
    // update
    Task<TDto> UpdateAsync(TDto dto);
    // delete
    Task<bool> DeleteAsync(int id);
}