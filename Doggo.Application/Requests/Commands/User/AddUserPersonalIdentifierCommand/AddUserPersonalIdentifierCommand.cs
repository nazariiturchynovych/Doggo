namespace Doggo.Application.Requests.Commands.User.AddUserPersonalIdentifierCommand;

using Domain.Enums;
using Domain.Results;
using MediatR;

public record AddUserPersonalIdentifierCommand(
    PersonalIdentifierType IdentifierType
) : IRequest<CommonResult>;