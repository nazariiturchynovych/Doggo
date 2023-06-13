namespace Doggo.Controllers;

using Application.Requests.Commands.User;
using Application.Requests.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserQuery(id), cancellationToken));
    }


    [HttpPut("UpdateAdmin")]
    public async Task<IActionResult> UpdateAdmin(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteUser/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteUserCommand(id), cancellationToken));
    }

}