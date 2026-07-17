using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Prime.Identity.Application.Abstractions;

namespace Prime.Identity.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _env;

    public ImageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveImageAsync(IFormFile file)
    {
        var folder = Path.Combine(_env.WebRootPath,"images");
        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(folder,fileName);

        using var stream = new FileStream(filePath,FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/images/{fileName}";
    }
}
