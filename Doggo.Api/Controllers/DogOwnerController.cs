namespace Doggo.Controllers;

using Application.Requests.Commands.DogOwner;
using Application.Requests.Queries.DogOwner;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "DogOwner, Admin")]
[Route("api/[Controller]")]
public class DogOwnerController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogOwnerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("CreateDogOwner")]
    public async Task<IActionResult> CreateDogOwner(CreateDogOwnerCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetDogOwner/{id:Guid}")]
    public async Task<IActionResult> GetDogOwner(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDogOwnerByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetCurrentDogOwner")]
    public async Task<IActionResult> GetCurrentDogOwner(CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetCurrentDogWalkerQuery(User.GetUserId()), cancellationToken));
    }

    [HttpGet("GetPageOfDogOwners")]
    public async Task<IActionResult> GetPageOfDogOwners(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfDogOwnersQuery(
                    nameSearchTerm,
                    sortColumn,
                    sortOrder,
                    pageCount,
                    page),
                cancellationToken));
    }

    [HttpPut("UpdateDogOwner")]
    public async Task<IActionResult> UpdateDogOwner(
        UpdateDogOwnerCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteDogOwner/{id:Guid}")]
    public async Task<IActionResult> DeleteDogOwner(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteDogOwnerCommand(id), cancellationToken));
    }
}