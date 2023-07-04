namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.User;
using Application.Requests.Commands.User.DeleteUserCommand;
using Application.Requests.Commands.User.UpdateUserCommand;
using Application.Requests.Queries.User;
using Application.Requests.Queries.User.GetPageOfUsersQuery;
using Application.Requests.Queries.User.GetUserQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[Controller]")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(id), cancellationToken));
    }

    [HttpGet("GetPageOfUsers")]
    public async Task<IActionResult> GetPageOfUsers(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfUsersQuery(
                    searchTerm,
                    sortColumn,
                    sortOrder,
                    pageCount,
                    page),
                cancellationToken));
    }


    [HttpPut("UpdateAdmin")]
    public async Task<IActionResult> UpdateAdmin(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteUser/{id:Guid}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteUserCommand(id), cancellationToken));
    }
}