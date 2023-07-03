namespace Doggo.Application.Requests.Commands.Image;

using System.Net;
using Amazon.S3.Model;
using Domain.Results;
using Infrastructure.Services.ImageService;
using MediatR;
using Microsoft.Extensions.Logging;

public record GetImageCommand(Guid Id) : IRequest<CommonResult<GetObjectResponse>>
{
    public class Handler : IRequestHandler<GetImageCommand, CommonResult<GetObjectResponse>>
    {
        private readonly IImageService _imageService;
        private readonly ILogger<Handler> _logger;

        public Handler(IImageService imageService, ILogger<Handler> logger )
        {
            _imageService = imageService;
            _logger = logger;
        }


        public async Task<CommonResult<GetObjectResponse>> Handle(GetImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _imageService.GetImageAsync(request.Id);

                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return Success(response);
                }

                return Failure<GetObjectResponse>(response.HttpStatusCode.ToString());

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Failure<GetObjectResponse>(e.Message);
            }

        }
    }
}