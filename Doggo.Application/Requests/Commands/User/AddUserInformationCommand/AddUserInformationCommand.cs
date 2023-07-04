namespace Doggo.Application.Requests.Commands.User.AddUserInformationCommand;

using Domain.Results;
using MediatR;

public record AddUserInformationCommand
(
    string FirstName,
    string LastName,
    int Age,
    string PhoneNumber
) : IRequest<CommonResult>;