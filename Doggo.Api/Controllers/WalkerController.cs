namespace Doggo.Controllers;

using Application.Requests.Commands.Walker;
using Application.Requests.Queries.Walker;
using Extensions;
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

    [AllowAnonymous]
    [HttpPost("CreateWalker")]
    public async Task<IActionResult> CreateWalker(CreateWalkerCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetWalker/{id:Guid}")]
    public async Task<IActionResult> GetWalker(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetWalkerByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetCurrentWalker")]
    public async Task<IActionResult> GetCurrentWalker(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCurrentWalkerQuery(User.GetUserId()), cancellationToken));
    }


    [HttpGet("GetPageOfWalkers")]
    public async Task<IActionResult> GetPageOfWalkers(
        string? nameSearchTerm,
        string? skillSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfWalkersQuery(
                    nameSearchTerm,
                    skillSearchTerm,
                    sortColumn,
                    sortOrder,
                    pageCount,
                    page),
                cancellationToken));
    }

    [HttpPut("UpdateWalker")]
    public async Task<IActionResult> UpdateWalker(
        UpdateWalkerCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteWalker/{id:Guid}")]
    public async Task<IActionResult> DeleteWalker(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteWalkerCommand(id), cancellationToken));
    }
}