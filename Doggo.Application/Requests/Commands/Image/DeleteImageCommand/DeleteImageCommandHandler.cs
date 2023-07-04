namespace Doggo.Application.Requests.Commands.Image.DeleteImageCommand;

using System.Net;
using Domain.Results;
using Infrastructure.Services.ImageService;
using MediatR;
using Microsoft.Extensions.Logging;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, CommonResult>
{
    private readonly IImageService _imageService;
    private readonly ILogger<DeleteImageCommandHandler> _logger;

    public DeleteImageCommandHandler(IImageService imageService, ILogger<DeleteImageCommandHandler> logger)
    {
        _imageService = imageService;
        _logger = logger;
    }


    public async Task<CommonResult> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _imageService.DeleteImageAsync(request.Id);

            return response.HttpStatusCode switch
            {
                HttpStatusCode.NoContent => Success(),
                _ => Failure(response.HttpStatusCode.ToString())
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Failure(e.Message);
        }
    }
}