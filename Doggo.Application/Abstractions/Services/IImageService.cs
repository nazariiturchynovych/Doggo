namespace Doggo.Application.Abstractions.Services;

using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

public interface IImageService
{
    Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file);

    Task<GetObjectResponse> GetImageAsync(Guid id);

    Task<DeleteObjectResponse> DeleteImageAsync(Guid id);
}