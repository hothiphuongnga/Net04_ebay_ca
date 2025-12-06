using CloudinaryDotNet;
using ebay.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet.Actions;

namespace ebay.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    // contructor
    private readonly Cloudinary _cloud;
    public CloudinaryService(IConfiguration configuration)
    {
        var setting = configuration.GetSection("Cloudinary");
        var cloudName = setting["CloudName"];
        var apiKey = setting["ApiKey"];
        var apiSecret = setting["ApiSecret"];
        var account = new Account(cloudName, apiKey, apiSecret);
        _cloud = new Cloudinary(account);
        _cloud.Api.Secure = true; // sử dụng HTTPS
    }

    public string UploadAsync(IFormFile file)
    {
        // kiểm tra file null
        if (file == null || file.Length == 0)
            throw new ArgumentException("File không được để trống", nameof(file));
        using var stream = file.OpenReadStream();
        // bt buoi 10 : false

        if (stream == null)
            throw new ArgumentException("Không thể đọc file", nameof(file));
        // chuẩn bị tham số upload
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "ebay_uploads", // thư mục lưu trữ trên Cloudinary
            UseFilename = true,
            UniqueFilename = false,
        };

        // upload
        var uploadresult = _cloud.Upload(uploadParams);
        if (uploadresult.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new Exception("Upload file thất bại: " + uploadresult.Error?.Message);
        }
        var res = uploadresult.SecureUrl.ToString();
        return res;

    }
}