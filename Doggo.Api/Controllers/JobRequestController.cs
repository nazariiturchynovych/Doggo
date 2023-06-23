namespace Doggo.Controllers;


using Application.Requests.Commands.JobRequest;
using Application.Requests.Queries.JobRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "DogOwner, Admin")]
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


    [HttpGet("GetJobRequest/{id:Guid}")]
    public async Task<IActionResult> GetJobRequest(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetJobRequestByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetDogOwnerJobRequests")]
    public async Task<IActionResult> GetDogOwnerJobRequests(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDogOwnerJobRequestsQuery(dogOwnerId), cancellationToken));
    }

    [HttpGet("GetPageOfJobRequests")]
    public async Task<IActionResult> GetPageOfJobRequests(
        string? descriptionSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfJobRequestsQuery(
                    descriptionSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken));
    }

    [HttpPut("UpdateJobRequest")]
    public async Task<IActionResult> UpdateJobRequest(
        UpdateJobRequestCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteJobRequest/{id:Guid}")]
    public async Task<IActionResult> DeleteJobRequest(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteJobRequestCommand(id), cancellationToken));
    }
}