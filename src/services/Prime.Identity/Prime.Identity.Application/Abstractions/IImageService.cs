using Microsoft.AspNetCore.Http;

namespace Prime.Identity.Application.Abstractions;

public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile file);
}
