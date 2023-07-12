namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Authentication.ApproveUserCommand;
using Application.Requests.Commands.User.DeleteUserCommand;
using Application.Requests.Commands.User.UpdateUserCommand;
using Application.Requests.Queries.User.GetPageOfUsersQuery;
using Application.Requests.Queries.User.GetUserQuery;
using Application.Responses.User;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(CommonResult<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetUserQuery(id), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetPageOfUsers")]
    [ProducesResponseType(typeof(CommonResult<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfUsers(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetPageOfUsersQuery(
                    searchTerm,
                    sortColumn,
                    sortOrder,
                    pageCount,
                    page),
                cancellationToken)).ToActionResult();
    }

    [HttpPost("ApproveUser")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApproveUser(
        ApproveUserCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }


    [HttpPut("UpdateAdmin")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAdmin(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeleteUser/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteUserCommand(id), cancellationToken)).ToActionResult();
    }
}