namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.PossibleSchedule.CreatePossibleScheduleCommand;
using Application.Requests.Commands.PossibleSchedule.DeletePossibleScheduleCommand;
using Application.Requests.Queries.PossibleSchedule.GetPageOfPossibleSchedulesQuery;
using Application.Requests.Queries.PossibleSchedule.GetPossibleScheduleByIdQuery;
using Application.Requests.Queries.PossibleSchedule.GetWalkersPossibleSchedulesQuery;
using Application.Responses;
using Application.Responses.Walker.PossibleSchedule;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePossibleSchedule(CreatePossibleScheduleCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpGet("GetPossibleSchedule/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult<PossibleScheduleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPossibleSchedule(Guid id,CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetPossibleScheduleByIdQuery(id), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetWalkerPossibleSchedules")]
    [ProducesResponseType(typeof(CommonResult<List<PossibleScheduleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWalkerPossibleSchedules(Guid walkerId,CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetWalkersPossibleSchedulesQuery(walkerId), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetPageOfPossibleSchedules")]
    [ProducesResponseType(typeof(CommonResult<PageOf<PossibleScheduleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfPossibleSchedules(
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetPageOfPossibleSchedulesQuery(pageCount, page), cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeletePossibleSchedule/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePossibleSchedule(Guid id,CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeletePossibleScheduleCommand(id), cancellationToken)).ToActionResult();
    }
}