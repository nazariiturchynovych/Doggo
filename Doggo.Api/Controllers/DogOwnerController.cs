namespace Doggo.Controllers;

using Application.Requests.Commands.DogOwner;
using Application.Requests.Queries.DogOwner;
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

    [HttpPost("CreateDogOwner")]
    public async Task<IActionResult> CreateDogOwner(CreateDogOwnerCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetDogOwner/{id:int}")]
    public async Task<IActionResult> GetDogOwner(int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDogOwnerByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetPageOfDogOwners")]
    public async Task<IActionResult> GetPageOfDogOwners(
        string searchTerm,
        string sortColumn,
        string sortOrder,
        int count,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfDogOwnersQuery(
                    searchTerm,
                    sortColumn,
                    sortOrder,
                    count,
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

    [HttpDelete("DeleteDogOwner/{id:int}")]
    public async Task<IActionResult> DeleteDogOwner(int id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteDogOwnerCommand(id), cancellationToken));
    }
}