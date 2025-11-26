using ebay.Domain.Entities;
namespace ebay.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> Get20ProductsAsync();
    //IEnumerable : đại diện cho một tập hợp các đối tượng có thể được lặp qua.
    // .ToList() : chuyển đổi IEnumerable thành List
    // crud
    // -->  repobase<T> =>

    // paging , trang nào , lấy bao nhiêu , tìm search gì
    // Task<PagingResult<Product>> GetProductsPagingAsync(int pageIndex, int pageSize, string? search);

    //update số lượng sản phẩm
    Task<bool> UpdateStock(int productId, int quantity);
}
