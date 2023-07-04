namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.PossibleSchedule;
using Application.Requests.Commands.PossibleSchedule.CreatePossibleScheduleCommand;
using Application.Requests.Commands.PossibleSchedule.DeletePossibleScheduleCommand;
using Application.Requests.Queries.PossibleSchedule.GetPageOfPossibleSchedulesQuery;
using Application.Requests.Queries.PossibleSchedule.GetPossibleScheduleByIdQuery;
using Application.Requests.Queries.PossibleSchedule.GetWalkersPossibleSchedulesQuery;
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
        return Ok(await _mediator.Send(new GetPageOfPossibleSchedulesQuery(pageCount, page), cancellationToken));
    }

    [HttpDelete("DeletePossibleSchedule/{id:Guid}")]
    public async Task<IActionResult> DeletePossibleSchedule(Guid id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeletePossibleScheduleCommand(id), cancellationToken));
    }
}