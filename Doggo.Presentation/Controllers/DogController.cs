namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Dog.CreateDogCommand;
using Application.Requests.Commands.Dog.DeleteDogCommand;
using Application.Requests.Commands.Dog.UpdateDogCommand;
using Application.Requests.Commands.Image.DeleteImageCommand;
using Application.Requests.Commands.Image.GetImageCommand;
using Application.Requests.Commands.Image.UploadImageCommand;
using Application.Requests.Queries.Dog.GetDogByIdQuery;
using Application.Requests.Queries.Dog.GetDogOwnerDogsQuery;
using Application.Requests.Queries.Dog.GetPageOfDogsQuery;
using Application.Responses;
using Application.Responses.Dog;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
[AllowAnonymous]
public class DogController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateDog")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDog(CreateDogCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpGet("GetDog/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult<DogResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDog(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetDogByIdQuery(id), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetDogOwnersDogs/{dogOwnerId:Guid}")]
    [ProducesResponseType(typeof(CommonResult<List<DogResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDogOwnersDogs(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetDogOwnerDogsQuery(dogOwnerId), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetPageOfDogs")]
    [ProducesResponseType(typeof(CommonResult<PageOf<DogResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfDogs(
        string? nameSearchTerm,
        string? descriptionSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetPageOfDogsQuery(
                    nameSearchTerm,
                    descriptionSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken)).ToActionResult();
    }

    [HttpPut("UpdateDog")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDog(
        UpdateDogCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeleteDog/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteDog(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteDogCommand(id), cancellationToken)).ToActionResult();
    }
    
    [HttpPost("Dog/{id:Guid}/Image")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadDogImage(Guid id, IFormFile file, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new UploadImageCommand(id, file), cancellationToken)).ToActionResult();
    }

    [HttpGet("Dog/{id:Guid}/Image")]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDogImage(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetImageCommand(id), cancellationToken);

        return File(result.Data.ResponseStream, result.Data.Headers.ContentType);
    }

    [HttpDelete("Dog/{id:Guid}/Image")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteDogImage(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteImageCommand(id), cancellationToken)).ToActionResult();
    }
}