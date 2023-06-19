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

    [HttpGet("GetPossibleSchedule/{id:int}")]
    public async Task<IActionResult> GetPossibleSchedule(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPossibleScheduleByIdQuery(id), cancellationToken));
    }
    [HttpGet("GetPageOfPossibleSchedules")]
    public async Task<IActionResult> GetPageOfPossibleSchedules(
        int count,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPageOfPossibleScheduleQuery(count, page), cancellationToken));
    }

    [HttpDelete("DeletePossibleSchedule/{id:int}")]
    public async Task<IActionResult> DeletePossibleSchedule(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeletePossibleScheduleCommand(id), cancellationToken));
    }
}