using AutoMapper;
using ebay.Application.DTOs;
using ebay.Application.Interfaces;
using ebay.Domain.Entities;
using ebay.Domain.Interfaces;
using ebay.Domain.ValueObjects;
using ebay.Shared.Common;

namespace ebay.Application.Services;
public class ProductService : ServiceBase<Product,ProductDTO>,IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper ) : base(repo, mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductDTO>> Get20ProductsAsync()
    {
        IEnumerable<Product> products = await _repo.Get20ProductsAsync();
        var res = _mapper.Map<List<ProductDTO>>(products);
        return res;
    }
    
    // public async Task<PagingResult<ProductDTO>> GetProductsPagingAsync(int pageIndex, int pageSize, string? search)
    // {
    //         // bọc code trong try catch để bắt lỗi
    //         PagingResult<Product> pagingProducts = await _repo.GetProductsPagingAsync(pageIndex, pageSize, search);
    //         ///TẠO LỖI 

    //         // int a = 10; int b = 0;
    //         // int c = a / b;

    //         /// 
    //         /// 
    //         // map từng phần
    //         var res = new PagingResult<ProductDTO>();
    //         res.PageIndex = pagingProducts.PageIndex;
    //         res.PageSize = pagingProducts.PageSize;
    //         res.TotalRow = pagingProducts.TotalRow;
    //         // chuyển danh sách sản phẩm sang DTO
    //         res.TotalItems = _mapper.Map<List<ProductDTO>>(pagingProducts.TotalItems);
    //         return res;
   
       


    // }


    // thêm người dùng 
    // public async Task AddUserDemo()
    // {
    //     User a = new User();
    //     a.Email = new Email("123123").ToString();

    // }
}