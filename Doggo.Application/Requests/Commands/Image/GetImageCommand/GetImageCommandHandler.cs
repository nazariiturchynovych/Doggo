namespace Doggo.Application.Requests.Commands.Image.GetImageCommand;

using System.Net;
using Abstractions.Services;
using Amazon.S3.Model;
using Domain.Results;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetImageCommandHandler : IRequestHandler<GetImageCommand, CommonResult<GetObjectResponse>>
{
    private readonly IImageService _imageService;
    private readonly ILogger<GetImageCommandHandler> _logger;

    public GetImageCommandHandler(IImageService imageService, ILogger<GetImageCommandHandler> logger )
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