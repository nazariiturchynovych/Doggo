namespace Doggo.Application.Requests.Commands.Image;

using System.Net;
using Doggo.Domain.Results;
using Infrastructure.Services.ImageService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public record UploadImageCommand(Guid Id, IFormFile File) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UploadImageCommand, CommonResult>
    {
        private readonly IImageService _imageService;
        private readonly ILogger<Handler> _logger;

        public Handler(IImageService imageService, ILogger<Handler> logger)
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
}