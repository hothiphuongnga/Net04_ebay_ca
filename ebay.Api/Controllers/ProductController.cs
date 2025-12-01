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
        
    

    // get by id
    [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _ser.GetByIdAsync(id);
            return Ok(new ResponseEntity<ProductDTO> 
            {
                Success = true,
                Message = "Lấy sản phẩm thành công",
                Content = res
            });
        }
        // get all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _ser.GetAllAsync();
            return Ok(new ResponseEntity<IEnumerable<ProductDTO>>
            {
                Success = true,
                Message = "Lấy tất cả sản phẩm thành công",
                Content = res
            });
        }
    }


}