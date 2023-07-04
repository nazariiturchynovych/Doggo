namespace Doggo.Application.Requests.Commands.Image.GetImageCommand;

using Amazon.S3.Model;
using Domain.Results;
using MediatR;

public record GetImageCommand(Guid Id) : IRequest<CommonResult<GetObjectResponse>>;