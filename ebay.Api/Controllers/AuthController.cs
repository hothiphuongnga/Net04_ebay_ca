namespace ebay.Api.Controllers
{
    using ebay.Application.DTOs;
    using ebay.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    // dungf iuserservice de dang ky, dang nhap
    public class AuthController(IUserService _userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
        {
           // gọi service để đăng ký
              var result = await _userService.RegisterUserAsync(dto);
              if(result.StatusCode == 200 || result.StatusCode == 201)
                {
                    return Ok(result);
                }
              return BadRequest(result);
        }
    }
}