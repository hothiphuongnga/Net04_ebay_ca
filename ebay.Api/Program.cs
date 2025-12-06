using System.Security.Claims;
using System.Text;
using System.Text.Json;
using ebay.Api.Filters;
using ebay.Api.Middlewares;
using ebay.Application.Interfaces;
using ebay.Application.Services;
using ebay.Infrastructure.Data;
using ebay.Domain.Interfaces;
using ebay.Domain.Repositories;
using ebay.Infrastructure.Repositories;
using ebay.Infrastructure.Services;
using ebay.Shared.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;


// using ebay.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// === ĐĂNG KÝ CÁC SERVICE (DEPENDENCY INJECTION) ===

// Đăng ký DbContext, cấu hình sử dụng SQL Server với chuỗi kết nối từ appsettings.json
builder.Services.AddDbContext<EBayDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
    options.UseSqlServer(connectionString);
});


builder.Services.AddControllers(op =>
{
    op.Filters.Add<XssProtectionFilter>();
});         // Hỗ trợ API Controllers
builder.Services.AddSwaggerGen();          // Hỗ trợ Swagger (OpenAPI) cho tài liệu API



// DI đăng ký các service và repository
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IUserService,UserService>();

builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();

builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Đăng ký AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(RatingMapper));

// === Câu hình AUTHEN, AUTHOR ===
var privateKey = builder.Configuration["jwt:Serect-Key"];
var Issuer = builder.Configuration["jwt:Issuer"];
var Audience = builder.Configuration["jwt:Audience"];

// cấu hình cơ bản
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Thiết lập các tham số xác thực token
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // Kiểm tra và xác nhận Issuer (nguồn phát hành token)
        ValidateIssuer = true,
        ValidIssuer = Issuer, // Biến `Issuer` chứa giá trị của Issuer hợp lệ
                              // Kiểm tra và xác nhận Audience (đối tượng nhận token)
        ValidateAudience = true,
        ValidAudience = Audience, // Biến `Audience` chứa giá trị của Audience hợp lệ
                                  // Kiểm tra và xác nhận khóa bí mật được sử dụng để ký token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey)),
        // Sử dụng khóa bí mật (`privateKey`) để tạo SymmetricSecurityKey nhằm xác thực chữ ký của token
        // Giảm độ trễ (skew time) của token xuống 0, đảm bảo token hết hạn chính xác
        ClockSkew = TimeSpan.Zero,
        // Xác định claim chứa vai trò của user (để phân quyền)
        RoleClaimType = ClaimTypes.Role,
        // Xác định claim chứa tên của user
        NameClaimType = ClaimTypes.Name,
        // Kiểm tra thời gian hết hạn của token, không cho phép sử dụng token hết hạn
        ValidateLifetime = true
    };
    // cấu hình response theo chuẩn ResponseEntity của dự án
    options.Events = new JwtBearerEvents
    {
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden; // 403 => không có quyền , 401 => chưa xác thực
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(ResponseEntity<string>.Fail("Bạn không có quyền truy cập tài nguyên này.", 403),
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return context.Response.WriteAsync(response);
        },
        OnChallenge = context => // khi không có token hoặc token không hợp lệ
        {
            context.HandleResponse(); // 
            context.Response.StatusCode = StatusCodes.Status401Unauthorized; // 401
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(ResponseEntity<string>.Fail("Yêu cầu xác thực. Vui lòng đăng nhập.", 401),
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return context.Response.WriteAsync(response);
        }
    };

});


builder.Services.AddAuthorization();

// Đăng ký Middleware BlockIpMiddleWare
builder.Services.AddScoped<BlockIpMiddleWare>();


// DI  FILTER
builder.Services.AddScoped<LogActionFilter>();

builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddScoped<AuthFilter>();

builder.Services.AddScoped<ResourceFilter>();
builder.Services.AddScoped<ResultFilter>();



// DI Memorycache
builder.Services.AddMemoryCache();

var app = builder.Build();

// === CẤU HÌNH MIDDLEWARE PIPELINE ===

// Kích hoạt Swagger & giao diện Swagger UI cho API docs & thử nghiệm
app.UseSwagger();
app.UseSwaggerUI();

// Tự động chuyển hướng HTTP sang HTTPS (bảo mật)
app.UseHttpsRedirection();

// Cho phép truy cập các file tĩnh (CSS, JS, ảnh, ...)
app.UseStaticFiles();

// Kích hoạt định tuyến
app.UseRouting();

// Map các endpoint cho Controller API, RazorPages, Blazor và fallback
app.MapControllers();

app.Run();