using ebay.Application.DTOs;
using ebay.Domain.Entities;
using ebay.Shared.Common;
using Microsoft.AspNetCore.Http;

namespace ebay.Application.Interfaces;
public interface IProductService : IServiceBase<Product, ProductDTO>
{
    Task<IEnumerable<ProductDTO>> Get20ProductsAsync();
    // Task<PagingResult<ProductDTO>> GetProductsPagingAsync(int pageIndex, int pageSize, string? search);
    Task<ResponseEntity<ProductDTO>> InsertProductWithImagesAsync(ProductCreateDTO productCreateDTO);
    Task<ResponseEntity<ProductDTO>> InsertProductWithImagesFileAsync(ProductCreateDTOV2 dto);



    // test upload cloud
    Task<ResponseEntity<string>> TestUploadCLoud(IFormFile file);
    Task<ResponseEntity<ProductDTO>> AddProductCloud(ProductCreateDTOV2 dto);
}