using Microsoft.AspNetCore.Http;

namespace ebay.Application.Interfaces;

public interface ICloudinaryService
{

    string UploadAsync(IFormFile file);
}