namespace Doggo.Controllers;

using Application.Requests.Commands.User;
using Application.Requests.Queries.User;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "User, Admin")]
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
                    pageCount,
                    page),
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