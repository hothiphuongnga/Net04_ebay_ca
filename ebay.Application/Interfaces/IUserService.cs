using ebay.Application.DTOs;
using ebay.Shared.Common;

namespace ebay.Application.Interfaces;
public interface IUserService
{
    Task<ResponseEntity<UserDTO>> RegisterUserAsync(UserRegisterDTO userRegisterDTO);
}