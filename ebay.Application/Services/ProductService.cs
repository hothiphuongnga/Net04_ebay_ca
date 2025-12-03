using AutoMapper;
using ebay.Application.DTOs;
using ebay.Application.Interfaces;
using ebay.Domain.Entities;
using ebay.Domain.Interfaces;
using ebay.Domain.ValueObjects;
using ebay.Shared.Common;
using Microsoft.AspNetCore.Http;

namespace ebay.Application.Services;

public class ProductService : ServiceBase<Product, ProductDTO>, IProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper) : base(repo, mapper)
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

    public async Task<ResponseEntity<ProductDTO>> InsertProductWithImagesAsync(ProductCreateDTO productCreateDTO)
    {
        try
        {
            Product product = _mapper.Map<Product>(productCreateDTO);
            // map thoong tin co ban

            // xuwr lys hinh anh
            foreach (var imageUrl in productCreateDTO.Images)
            {
                //https://dummyimage.com/600x400/999799/95ff00&text=Smartwatch%2019
                ProductImage productImage = new ProductImage()
                {
                    Id = 0,
                    ImageUrl = imageUrl,
                    IsPrimary = false,
                    ProductId = product.Id,
                    CreatedAt = DateTime.Now,
                    Deleted = false
                };
                // thâm vào list hình ảnh của sản phẩm
                product.AddProductImage(productImage);
            }
            // lưu sản phẩm vào db
            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();

            var res = _mapper.Map<ProductDTO>(product);
            return new ResponseEntity<ProductDTO>()
            {
                Content = res,
                Success = true,
                Message = "Tạo sản phẩm thành công",
                StatusCode = 201
            };
        }
        catch (Exception ex)
        {
            return new ResponseEntity<ProductDTO>()
            {
                Content = null,
                Success = false,
                Message = "Lỗi khi tạo sản phẩm: " + ex.Message,
                StatusCode = 500
            };
        }

    }

    public async Task<ResponseEntity<ProductDTO>> InsertProductWithImagesFileAsync(ProductCreateDTOV2 dto)
    {
        try
        {
                 // chuyeern veef Product
        Product product = _mapper.Map<Product>(dto);
        // xuwr lys ds file
        foreach (IFormFile file in dto.Images)
        {
            // xử lý lưu file lên server, lấy đường dẫn
            string imageUrl = await SaveFileAsync(file);
            ProductImage productImage = new ProductImage()
            {
                Id = 0,
                ImageUrl = imageUrl,
                IsPrimary = false,
                ProductId = product.Id,
                CreatedAt = DateTime.Now,
                Deleted = false
            };
            // thâm vào list hình ảnh của sản phẩm
            product.AddProductImage(productImage);
        }
        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync(); 


        return new ResponseEntity<ProductDTO>()
        {
            Content = _mapper.Map<ProductDTO>(product),
            Success = true,
            Message = "Tạo sản phẩm thành công",
            StatusCode = 201
        };
        }
        catch (Exception ex)
        {
            return new ResponseEntity<ProductDTO>()
            {
                Content = null,
                Success = false,
                Message = "Lỗi khi tạo sản phẩm: " + ex.Message,
                StatusCode = 500
            };
        }
    }
//funtion static net9 convert string to slug  
    private async Task<string> SaveFileAsync(IFormFile file)
    {
        // tạo tên file mới để tránh trùng lặp
        string newFileName = DateTime.Now.ToString("dd-mm-yyyy")+ "_" + file.FileName;
        // xác định đường dẫn lưu file
        string folderPath = "wwwroot/pnga";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string filePath = Path.Combine(folderPath, newFileName); // tương thích với hệ diều hành  \ /
        // lưu file lên server
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        // trả về đường dẫn truy cập file
        return "/pnga/" + newFileName;
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