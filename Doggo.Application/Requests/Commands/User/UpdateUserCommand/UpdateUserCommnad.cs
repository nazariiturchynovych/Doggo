namespace Doggo.Application.Requests.Commands.User.UpdateUserCommand;

using Domain.Results;
using MediatR;

public record UpdateUserCommand
(
    string? FirstName,
    string? LastName,
    int? Age,
    string? PhoneNumber
) : IRequest<CommonResult>;