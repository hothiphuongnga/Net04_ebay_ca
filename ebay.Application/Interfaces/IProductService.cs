using ebay.Application.DTOs;
using ebay.Shared.Common;

namespace ebay.Application.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductDTO>> Get20ProductsAsync();
    // Task<PagingResult<ProductDTO>> GetProductsPagingAsync(int pageIndex, int pageSize, string? search);
}