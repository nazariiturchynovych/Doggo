namespace Doggo.Controllers;

using Application.Requests.Commands.Job;
using Application.Requests.Queries.Job;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "DogOwner, Admin")]
[Route("api/[Controller]")]
public class JobController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateJob")]
    public async Task<IActionResult> CreateJob(CreateJobCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetJob/{id:int}")]
    public async Task<IActionResult> GetJob(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetJobByIdQuery(id), cancellationToken));
    }
    [HttpGet("GetPageOfJobs")]
    public async Task<IActionResult> GetPageOfJobs(
        int count,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPageOfJobsQuery(count, page), cancellationToken));
    }

    [HttpPut("UpdateJob")]
    public async Task<IActionResult> UpdateJob(
        UpdateJobCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteJob/{id:int}")]
    public async Task<IActionResult> DeleteJob(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteJobCommand(id), cancellationToken));
    }
}