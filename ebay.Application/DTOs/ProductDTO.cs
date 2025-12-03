namespace ebay.Application.DTOs;
using Microsoft.AspNetCore.Http;
public class ProductDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; } // stock : tồn kho 

    public virtual ICollection<ProductImageDTO> ProductImages { get; set; } = new List<ProductImageDTO>();
    public virtual ICollection<RatingDTO> Ratings { get; set; } = new List<RatingDTO>();
} 
// thêm DTO -> nhớ thêm map 
// demo lưu file ở FE 
// dường dẫn file sẽ là dường_dan_fe/ten_file
public class ProductCreateDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; } // stock : tồn kho 
    // ds hình ảnh 
    // http://localhost:5173/images/abc.jpg
    public List<string> Images { get; set;} = new List<string>();
}
public class ProductCreateDTOV2
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; } // stock : tồn kho 
    // ds hình ảnh 
    // http://localhost:5173/images/abc.jpg
    public List<IFormFile> Images { get; set;}
}