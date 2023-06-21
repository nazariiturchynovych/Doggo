namespace Doggo.Controllers;

using Application.Requests.Commands.Dog;
using Application.Requests.Queries.Dog;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "DogOwner, Admin")]
[Route("api/[Controller]")]
public class DogController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateDog")]
    public async Task<IActionResult> CreateDog(CreateDogCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetDog/{id:Guid}")]
    public async Task<IActionResult> GetDog(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDogByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetDogOwnersDogs")]
    public async Task<IActionResult> GetDogOwnersDogs(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDogOwnerDogsQuery(dogOwnerId), cancellationToken));
    }

    [HttpGet("GetPageOfDogs")]
    public async Task<IActionResult> GetPageOfDogs(
        string? nameSearchTerm,
        string? descriptionSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfDogsQuery(
                    nameSearchTerm,
                    descriptionSearchTerm,
                    sortColumn,
                    sortOrder,
                    pageCount,
                    page),
                cancellationToken));
    }

    [HttpPut("UpdateDog")]
    public async Task<IActionResult> UpdateDog(
        UpdateDogCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteDog/{id:Guid}")]
    public async Task<IActionResult> DeleteDog(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteDogCommand(id), cancellationToken));
    }
}