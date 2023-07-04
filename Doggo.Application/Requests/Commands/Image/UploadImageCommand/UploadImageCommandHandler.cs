namespace Doggo.Application.Requests.Commands.Image.UploadImageCommand;

using System.Net;
using Domain.Results;
using Infrastructure.Services.ImageService;
using MediatR;
using Microsoft.Extensions.Logging;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, CommonResult>
{
    private readonly IImageService _imageService;
    private readonly ILogger<UploadImageCommandHandler> _logger;

    public UploadImageCommandHandler(IImageService imageService, ILogger<UploadImageCommandHandler> logger)
    {
        _imageService = imageService;
        _logger = logger;
    }


    public async Task<CommonResult> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _imageService.UploadImageAsync(request.Id, request.File);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return Success();
            }

            return Failure(response.HttpStatusCode.ToString());
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
            return Failure(e.Message);
        }
    }
}