namespace Doggo.Controllers;

using Application.Requests.Commands.Walker;
using Application.Requests.Queries.Walker;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "Walker, Admin")]
[Route("api/[Controller]")]
public class WalkerController : ControllerBase
{
    private readonly IMediator _mediator;

    public WalkerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateWalker")]
    public async Task<IActionResult> CreateWalker(CreateWalkerCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetWalker/{id:int}")]
    public async Task<IActionResult> GetWalker(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetWalkerByIdQuery(id), cancellationToken));
    }
    [HttpGet("GetPageOfWalkers")]
    public async Task<IActionResult> GetPageOfWalkers(
        int count,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPageOfWalkerQuery(count, page), cancellationToken));
    }

    [HttpPut("UpdateWalker")]
    public async Task<IActionResult> UpdateWalker(
        UpdateWalkerCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteWalker/{id:int}")]
    public async Task<IActionResult> DeleteWalker(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteWalkerCommand(id), cancellationToken));
    }
}