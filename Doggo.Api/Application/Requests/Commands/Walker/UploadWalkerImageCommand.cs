namespace Doggo.Application.Requests.Commands.Walker;

using System.Net;
using Domain.Results;
using Infrastructure.Services.ImageService;
using MediatR;

public record UploadWalkerImageCommand(Guid Id, IFormFile File) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UploadWalkerImageCommand, CommonResult>
    {
        private readonly IImageService _imageService;

        public Handler(IImageService imageService)
        {
            _imageService = imageService;
        }


        public async Task<CommonResult> Handle(UploadWalkerImageCommand request, CancellationToken cancellationToken)
        {
            var response = await _imageService.UploadImageAsync(request.Id, request.File);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return Success();
            }
            
            return Failure(response.HttpStatusCode.ToString());
        }
    }
}