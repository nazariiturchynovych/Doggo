namespace Doggo.Application.Requests.Commands.Image;

using System.Net;
using Doggo.Domain.Results;
using Infrastructure.Services.ImageService;
using MediatR;

public record DeleteImageCommand(Guid Id) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteImageCommand, CommonResult>
    {
        private readonly IImageService _imageService;
        private readonly ILogger<Handler> _logger;

        public Handler(IImageService imageService, ILogger<Handler> logger)
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
}