namespace Doggo.Presentation.Controllers;

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

    [HttpGet("GetJob/{id:Guid}")]
    public async Task<IActionResult> GetJob(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetJobByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetDogOwnerJobs")]
    public async Task<IActionResult> GetDogOwnerJobs(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDogOwnerJobsQuery(dogOwnerId), cancellationToken));
    }
    [HttpGet("GetWalkerJobs")]
    public async Task<IActionResult> GetWalkerJobs(
        Guid walkerId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetWalkerJobsQuery(walkerId), cancellationToken));
    }
    [HttpGet("GetDogJobs")]
    public async Task<IActionResult> GetDogJobs(
        Guid dogId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDogJobsQuery(dogId), cancellationToken));
    }

    [HttpGet("GetPageOfJobs")]
    public async Task<IActionResult> GetPageOfJobs(
        string? commentSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfJobsQuery(
                    commentSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken));
    }

    [HttpPost("ApplyJob")]
    public async Task<IActionResult> ApplyJob(ApplyJobCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPost("DeclineJob")]
    public async Task<IActionResult> DeclineJob(DeclineJobCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }


    [HttpPut("UpdateJob")]
    public async Task<IActionResult> UpdateJob(
        UpdateJobCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteJob/{id:Guid}")]
    public async Task<IActionResult> DeleteJob(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteJobCommand(id), cancellationToken));
    }
}