namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Authentication.ChangePasswordCommand;
using Application.Requests.Commands.User.AddUserPersonalIdentifierCommand;
using Application.Requests.Commands.User.DeleteUserCommand;
using Application.Requests.Commands.User.UpdateUserCommand;
using Application.Requests.Queries.User.GetPageOfUsersQuery;
using Application.Requests.Queries.User.GetUserQuery;
using Application.Responses;
using Application.Responses.User;
using Domain.Constants;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
[Authorize(Roles = $"{RoleConstants.User}")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser(Guid? id,CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetUserQuery(id ?? User.GetUserId()), cancellationToken)).ToActionResult();
    }

    [HttpGet("user")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetUserQuery(User.GetUserId()), cancellationToken)).ToActionResult();
    }

    [HttpGet("users")]
    [ProducesResponseType(typeof(CommonResult<PageOf<UserResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfUsers(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetPageOfUsersQuery(
                    nameSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken)).ToActionResult();
    }

    [HttpPut("user")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [Authorize(Roles = $"{RoleConstants.User}, {RoleConstants.Admin}")]
    [HttpPut("password")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordCommand changePasswordCommand,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(changePasswordCommand, cancellationToken)).ToActionResult();
    }


    [HttpDelete("user")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser( CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteUserCommand(User.GetUserId()), cancellationToken)).ToActionResult();
    }

    [HttpPost("user/personal-identifier")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUserPersonalIdentifier(
        AddUserPersonalIdentifierCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }
}