namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.JobRequest.CreateJobRequestCommand;
using Application.Requests.Commands.JobRequest.DeleteJobRequestCommand;
using Application.Requests.Commands.JobRequest.UpdateJobRequestCommand;
using Application.Requests.Queries.JobRequest.GetDogOwnerJobRequestsQuery;
using Application.Requests.Queries.JobRequest.GetJobRequestByIdQuery;
using Application.Requests.Queries.JobRequest.GetPageOfJobRequestsQuery;
using Application.Responses;
using Application.Responses.JobRequest;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateJobRequest(CreateJobRequestCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }


    [HttpGet("GetJobRequest/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult<JobRequestResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetJobRequest(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetJobRequestByIdQuery(id), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetDogOwnerJobRequests")]
    [ProducesResponseType(typeof(CommonResult<List<JobRequestResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDogOwnerJobRequests(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetDogOwnerJobRequestsQuery(dogOwnerId), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetPageOfJobRequests")]
    [ProducesResponseType(typeof(CommonResult<PageOf<JobRequestResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfJobRequests(
        string? descriptionSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetPageOfJobRequestsQuery(
                    descriptionSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken)).ToActionResult();
    }

    [HttpPut("UpdateJobRequest")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateJobRequest(
        UpdateJobRequestCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeleteJobRequest/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteJobRequest(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteJobRequestCommand(id), cancellationToken)).ToActionResult();
    }
}