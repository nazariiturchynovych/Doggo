namespace Doggo.Controllers;

using Application.Requests.Commands.User;
using Application.Requests.Queries.User;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "User")]
    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(User.GetUserId()), cancellationToken));
    }

    [HttpGet("GetPageOfUsers")]
    public async Task<IActionResult> SendEmailVerificationToken(
        int count,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPageOfUsersQuery(count, page), cancellationToken));
    }


    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteUserCommand(User.GetUserId()), cancellationToken));
    }
}