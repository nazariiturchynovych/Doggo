namespace Doggo.Application.Requests.Commands.Authentication.SignUpCommand;

using Domain.Results;
using MediatR;

public record SignUpCommand
(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    int Age,
    string PhoneNumber
) : IRequest<CommonResult<Guid>>;