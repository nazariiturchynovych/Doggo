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

    [HttpGet("GetDog/{id:int}")]
    public async Task<IActionResult> GetDog(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDogByIdQuery(id), cancellationToken));
    }
    [HttpGet("GetPageOfDogs")]
    public async Task<IActionResult> GetPageOfDogs(
        int count,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetPageOfDogsQuery(count, page), cancellationToken));
    }

    [HttpPut("UpdateDog")]
    public async Task<IActionResult> UpdateDog(
        UpdateDogCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteDog/{id:int}")]
    public async Task<IActionResult> DeleteDog(int id,CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteDogCommand(id), cancellationToken));
    }
}