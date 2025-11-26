namespace ebay.Api.Controllers
{
    using Azure;
    using ebay.Application.DTOs;
    using ebay.Application.Interfaces;
    using ebay.Shared.Common;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _ser) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _ser.Get20ProductsAsync();
            return Ok(new ResponseEntity<IEnumerable<ProductDTO>>
            {
                Success = true,
                Message = "Lấy danh sách sản phẩm thành công",
                Content = res
            });
        }
    }
}