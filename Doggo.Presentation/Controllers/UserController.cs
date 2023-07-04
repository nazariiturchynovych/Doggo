namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.User;
using Application.Requests.Commands.User.AddUserInformationCommand;
using Application.Requests.Commands.User.AddUserPersonalIdentifierCommand;
using Application.Requests.Commands.User.DeleteUserCommand;
using Application.Requests.Commands.User.UpdateUserCommand;
using Application.Requests.Queries.User;
using Application.Requests.Queries.User.GetPageOfUsersQuery;
using Application.Requests.Queries.User.GetUserQuery;
using Extensions;
using MediatR;
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
    public async Task<IActionResult> GetUser(Guid id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(id), cancellationToken));
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(User.GetUserId()), cancellationToken));
    }

    [HttpGet("GetPageOfUsers")]
    public async Task<IActionResult> GetPageOfUsers(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfUsersQuery(
                    nameSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken));
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPost("AddUserInformation")]
    public async Task<IActionResult> AddUserInformation(
        AddUserInformationCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteUserCommand(User.GetUserId()), cancellationToken));
    }

    [HttpPost("AddPersonalIdentifier")]
    public async Task<IActionResult> AddUserPersonalIdentifier(
        AddUserPersonalIdentifierCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}