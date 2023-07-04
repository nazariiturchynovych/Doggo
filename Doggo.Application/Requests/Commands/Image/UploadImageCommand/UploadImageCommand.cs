namespace Doggo.Application.Requests.Commands.Image.UploadImageCommand;

using Domain.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

public record UploadImageCommand(Guid Id, IFormFile File) : IRequest<CommonResult>;