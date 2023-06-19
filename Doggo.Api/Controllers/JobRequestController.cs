namespace Doggo.Controllers;

using Application.Requests.Commands.JobRequest;
using Application.Requests.Queries.JobRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "JobRequest, Admin")]
[Route("api/[Controller]")]
public class JobRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateJobRequest")]
    public async Task<IActionResult> CreateJobRequest(CreateJobRequestCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetJobRequest/{id:int}")]
    public async Task<IActionResult> GetJobRequest(int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetJobRequestByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetPageOfJobRequests")]
    public async Task<IActionResult> GetPageOfJobRequests(
        int count,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPageOfJobRequestsQuery(count, page), cancellationToken));
    }

    [HttpPut("UpdateJobRequest")]
    public async Task<IActionResult> UpdateJobRequest(
        UpdateJobRequestCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteJobRequest/{id:int}")]
    public async Task<IActionResult> DeleteJobRequest(int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteJobRequestCommand(id), cancellationToken));
    }
}