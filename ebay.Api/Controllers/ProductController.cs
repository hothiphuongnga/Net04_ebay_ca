namespace ebay.Api.Controllers
{
    using Azure;
    using ebay.Api.Filters;
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

        // api tạo sp mới 
        [HttpPost("strUrl")]
        public async Task<IActionResult> PostProduct([FromBody] ProductCreateDTO dto)
        {
            var res = await _ser.InsertProductWithImagesAsync(dto);
            return res.StatusCode switch
            {
                201 =>Ok(res),
                400 => BadRequest(res),
                _ => StatusCode(500, res)
            };
            
            // 
            
        }
        [HttpPost("formFile")]
        public async Task<IActionResult> PostProductFile([FromForm] ProductCreateDTOV2 dto)
        {
            // tạo IProductService.InsertProductWithImagesFileAsync
            // thực thi 
            var res = await _ser.InsertProductWithImagesFileAsync(dto);
            return res.StatusCode switch
            {
                201 =>Ok(res),
                400 => BadRequest(res),
                _ => StatusCode(500, res)
            };
            
            // 
            
        }
        [HttpPost("test-upload-cloud")]
        //
        public async Task<IActionResult> PostProductFile(IFormFile file)
        {
            var res = await _ser.TestUploadCLoud(file);
            return res.StatusCode switch
            {
                200 =>Ok(res),
                400 => BadRequest(res),
                _ => StatusCode(500, res)
            };
            
            // 
            
        }

        [HttpPost("add-product-cloud")]
        public async Task<IActionResult> PostProductFileCloud(ProductCreateDTOV2 file)
        {
            var res = await _ser.AddProductCloud(file);
            return res.StatusCode switch
            {
                201 =>Ok(res), 0 => BadRequest(res),
                _ => StatusCode(500, res)
            };
        }
//api test
        [HttpGet("test-product")]
        public IActionResult GetProducttest()
        {
            return Ok();
        }
    }


}

