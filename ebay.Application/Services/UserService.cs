using AutoMapper;
using ebay.Application.DTOs;
using ebay.Application.Interfaces;
using ebay.Domain.Entities;
using ebay.Domain.Repositories;
using ebay.Shared.Common;

namespace ebay.Application.Services;

public class UserService : IUserService
{
    
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHelper _passwordHelper;

    public UserService( IMapper mapper, IUserRepository userRepo, IPasswordHelper passwordHelper )
    {
     
        _mapper = mapper;
        _userRepo = userRepo;
        _passwordHelper = passwordHelper;
    }

    public async Task<ResponseEntity<UserDTO>> RegisterUserAsync(UserRegisterDTO userRegisterDTO)
    {
        // chuyển đổi UserRegisterDTO thành User
        
        // gọi repo để lưu user vào db

        // kiểm tra username hoặc email đã tồn tại chưa
            // tạo function tron repo để kiểm tra 
            // gọi lại repo sử dụng




            // ++++++++++ CÁCH 1 dùng EF
            // tạo user mới thì chuyênr dto về User sau đó dùng _ contexxt add vào database
            User newUser = _mapper.Map<User>(userRegisterDTO);
            // băm mật khẩu
            newUser.PasswordHash = _passwordHelper.HashPassword(userRegisterDTO.Password);
            newUser.Deleted = false;
            // thêm user role
            var userRole = new UserRole
            {
                UserId = newUser.Id,
                RoleId = 6 // role user
            };
            newUser.UserRoles.Add(userRole);
            // lưu user vào database => gọi repo
            var createUser = await _userRepo.RegisterUserAsync(newUser);
            // trả về kết quả UserDTO
            var res = _mapper.Map<UserDTO>(createUser);
            return ResponseEntity<UserDTO>.Ok(res, "Đăng ký thành công");
    }
}