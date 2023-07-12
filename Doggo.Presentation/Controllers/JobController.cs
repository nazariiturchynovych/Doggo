namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Job.AcceptJobCommand;
using Application.Requests.Commands.Job.CreateAndApplyJobCommand;
using Application.Requests.Commands.Job.DeleteJobCommand;
using Application.Requests.Commands.Job.DoneJobCommand;
using Application.Requests.Commands.Job.RejectJobCommand;
using Application.Requests.Commands.Job.UpdateJobCommand;
using Application.Requests.Queries.Job.GetDogJobsQuery;
using Application.Requests.Queries.Job.GetDogOwnerJobsQuery;
using Application.Requests.Queries.Job.GetJobByIdQuery;
using Application.Requests.Queries.Job.GetPageOfJobsQuery;
using Application.Requests.Queries.Job.GetWalkerJobsQuery;
using Application.Responses;
using Application.Responses.Job;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    [HttpPost("CreateAndApplyJob")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateJob(CreateAndApplyJobCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpGet("GetJob/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult<JobResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetJob(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetJobByIdQuery(id), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetDogOwnerJobs")]
    [ProducesResponseType(typeof(CommonResult<List<JobResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDogOwnerJobs(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetDogOwnerJobsQuery(dogOwnerId), cancellationToken)).ToActionResult();
    }
    [HttpGet("GetWalkerJobs")]
    [ProducesResponseType(typeof(CommonResult<List<JobResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWalkerJobs(
        Guid walkerId,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetWalkerJobsQuery(walkerId), cancellationToken)).ToActionResult();
    }
    [HttpGet("GetDogJobs")]
    [ProducesResponseType(typeof(CommonResult<List<JobResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDogJobs(
        Guid dogId,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetDogJobsQuery(dogId), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetPageOfJobs")]
    [ProducesResponseType(typeof(CommonResult<PageOf<JobResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfJobs(
        string? commentSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetPageOfJobsQuery(
                    commentSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken)).ToActionResult();
    }

    [HttpPost("AcceptJob")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApplyJob(AcceptJobCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPost("RejectJob")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeclineJob(RejectJobCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPost("DoneJob")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeclineJob(DoneJobCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPut("UpdateJob")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateJob(
        UpdateJobCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeleteJob/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteJob(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteJobCommand(id), cancellationToken)).ToActionResult();
    }
}