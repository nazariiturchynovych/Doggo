namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.User.AddUserInformationCommand;
using Application.Requests.Commands.User.AddUserPersonalIdentifierCommand;
using Application.Requests.Commands.User.DeleteUserCommand;
using Application.Requests.Commands.User.UpdateUserCommand;
using Application.Requests.Queries.User.GetPageOfUsersQuery;
using Application.Requests.Queries.User.GetUserQuery;
using Application.Responses;
using Application.Responses.User;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetUser/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser(Guid id,CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetUserQuery(id), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetUser")]
    [ProducesResponseType(typeof(CommonResult<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetUserQuery(User.GetUserId()), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetPageOfUsers")]
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

    [HttpPut("UpdateUser")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPost("AddUserInformation")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUserInformation(
        AddUserInformationCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeleteUser")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteUserCommand(User.GetUserId()), cancellationToken)).ToActionResult();
    }

    [HttpPost("AddPersonalIdentifier")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUserPersonalIdentifier(
        AddUserPersonalIdentifierCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }
}