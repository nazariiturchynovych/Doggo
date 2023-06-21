namespace Doggo.Controllers;

using Application.Requests.Commands.Walker.PossibleSchedule;
using Application.Requests.Queries.Walker.PossibleSchedule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "Walker, Admin")]
[Route("api/[Controller]")]
public class PossibleScheduleController : ControllerBase
{
    private readonly IMediator _mediator;

    public PossibleScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreatePossibleSchedule")]
    public async Task<IActionResult> CreatePossibleSchedule(CreatePossibleScheduleCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetPossibleSchedule/{id:Guid}")]
    public async Task<IActionResult> GetPossibleSchedule(Guid id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPossibleScheduleByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetWalkerPossibleSchedules")]
    public async Task<IActionResult> GetWalkerPossibleSchedules(Guid walkerId,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetWalkersPossibleSchedulesQuery(walkerId), cancellationToken));
    }

    [HttpGet("GetPageOfPossibleSchedules")]
    public async Task<IActionResult> GetPageOfPossibleSchedules(
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPageOfPossibleScheduleQuery(pageCount, page), cancellationToken));
    }

    [HttpDelete("DeletePossibleSchedule/{id:Guid}")]
    public async Task<IActionResult> DeletePossibleSchedule(Guid id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeletePossibleScheduleCommand(id), cancellationToken));
    }
}