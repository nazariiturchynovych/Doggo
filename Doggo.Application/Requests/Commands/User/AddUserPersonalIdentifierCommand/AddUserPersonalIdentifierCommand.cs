namespace Doggo.Application.Requests.Commands.User.AddUserPersonalIdentifierCommand;

using Base;
using Domain.Enums;
using Domain.Results;

public record AddUserPersonalIdentifierCommand(
    PersonalIdentifierType IdentifierType
) : ICommand<CommonResult>;