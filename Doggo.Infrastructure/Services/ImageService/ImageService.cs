namespace Doggo.Infrastructure.Services.ImageService;

using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Options;

public class ImageService : IImageService
{
    private readonly IAmazonS3 _s3;
    private readonly IOptions<S3Options> _options;

    public ImageService(IAmazonS3 s3, IOptions<S3Options> options)
    {
        _s3 = s3;
        _options = options;
    }


    public async Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file)
    {
        var putObjectRequest = new PutObjectRequest()
        {
            BucketName = _options.Value.BucketName,
            Key = $"image/{id}",
            ContentType = file.ContentType,
            InputStream = file.OpenReadStream(),
            Metadata =
            {
                ["x-amz-meta-originalname"] = file.FileName,
                ["x-amz-meta-extension"] = Path.GetExtension(file.FileName)
            }
        };

        return await _s3.PutObjectAsync(putObjectRequest);
    }

    public async Task<GetObjectResponse> GetImageAsync(Guid id)
    {
        var getObjectRequest = new GetObjectRequest()
        {
            BucketName = _options.Value.BucketName,
            Key = $"image/{id}"
        };

        return await _s3.GetObjectAsync(getObjectRequest);
    }

    public async Task<DeleteObjectResponse> DeleteImageAsync(Guid id)
    {
        var deleteObjectRequest = new DeleteObjectRequest()
        {
            BucketName = _options.Value.BucketName,
            Key = $"image/{id}"
        };

        return await _s3.DeleteObjectAsync(deleteObjectRequest);
    }
}