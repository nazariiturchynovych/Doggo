namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.DogOwner.CreateDogOwnerCommand;
using Application.Requests.Commands.DogOwner.DeleteDogOwnerCommand;
using Application.Requests.Commands.DogOwner.UpdateDogOwnerCommand;
using Application.Requests.Commands.Image.DeleteImageCommand;
using Application.Requests.Commands.Image.GetImageCommand;
using Application.Requests.Commands.Image.UploadImageCommand;
using Application.Requests.Queries.DogOwner.GetCurrentDogOwnerQuery;
using Application.Requests.Queries.DogOwner.GetDogOwnerByIdQuery;
using Application.Requests.Queries.DogOwner.GetPageOfDogOwnersQuery;
using Application.Responses;
using Application.Responses.DogOwner;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
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
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDogOwner(CreateDogOwnerCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [Authorize(Roles = "DogOwner, Admin, User")]
    [HttpGet("GetDogOwner/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult<DogOwnerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDogOwner(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetDogOwnerByIdQuery(id), cancellationToken)).ToActionResult();
    }

    [Authorize(Roles = "DogOwner, Admin, User")]
    [HttpGet("GetCurrentDogOwner")]
    [ProducesResponseType(typeof(CommonResult<DogOwnerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCurrentDogOwner(CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetCurrentDogOwnerQuery(User.GetUserId()), cancellationToken)).ToActionResult();
    }

    [Authorize(Roles = "DogOwner, Admin, User")]
    [HttpGet("GetPageOfDogOwners")]
    [ProducesResponseType(typeof(CommonResult<PageOf<DogOwnerResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfDogOwners(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetPageOfDogOwnersQuery(
                    nameSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken)).ToActionResult();
    }


    [Authorize(Roles = "DogOwner, Admin")]
    [HttpPut("UpdateDogOwner")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDogOwner(
        UpdateDogOwnerCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [Authorize(Roles = "DogOwner, Admin")]
    [HttpDelete("DeleteDogOwner/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteDogOwner(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteDogOwnerCommand(id), cancellationToken)).ToActionResult();
    }

    [Authorize(Roles = "DogOwner, Admin, User")]
    [HttpPost("DogOwner/{id:Guid}/Image")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadDogOwnerImage(Guid id, IFormFile file, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new UploadImageCommand(id, file), cancellationToken)).ToActionResult();
    }

    [Authorize(Roles = "DogOwner, Admin, User")]
    [HttpGet("DogOwner/{id:Guid}/Image")]
    [ProducesResponseType(typeof(CommonResult<FileStreamResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDogOwnerImage(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetImageCommand(id), cancellationToken);

        return File(result.Data.ResponseStream, result.Data.Headers.ContentType);
    }

    [Authorize(Roles = "DogOwner, Admin, User")]
    [HttpDelete("DogOwner/{id:Guid}/Image")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteDogOwnerImage(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteImageCommand(id), cancellationToken)).ToActionResult();
    }
}