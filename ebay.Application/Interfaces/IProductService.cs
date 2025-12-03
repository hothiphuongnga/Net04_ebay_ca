using ebay.Application.DTOs;
using ebay.Domain.Entities;
using ebay.Shared.Common;

namespace ebay.Application.Interfaces;
public interface IProductService : IServiceBase<Product, ProductDTO>
{
    Task<IEnumerable<ProductDTO>> Get20ProductsAsync();
    // Task<PagingResult<ProductDTO>> GetProductsPagingAsync(int pageIndex, int pageSize, string? search);
    Task<ResponseEntity<ProductDTO>> InsertProductWithImagesAsync(ProductCreateDTO productCreateDTO);
    Task<ResponseEntity<ProductDTO>> InsertProductWithImagesFileAsync(ProductCreateDTOV2 dto);
}