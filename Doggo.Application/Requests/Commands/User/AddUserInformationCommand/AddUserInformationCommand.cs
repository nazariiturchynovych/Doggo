namespace Doggo.Application.Requests.Commands.User.AddUserInformationCommand;

using Base;
using Domain.Results;

public record AddUserInformationCommand
(
    string FirstName,
    string LastName,
    int Age,
    string PhoneNumber
) : ICommand<CommonResult>;