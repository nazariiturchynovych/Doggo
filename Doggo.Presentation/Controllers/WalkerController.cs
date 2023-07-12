namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Image.DeleteImageCommand;
using Application.Requests.Commands.Image.GetImageCommand;
using Application.Requests.Commands.Image.UploadImageCommand;
using Application.Requests.Commands.Walker.CreateWalkerCommand;
using Application.Requests.Commands.Walker.DeleteWalkerCommand;
using Application.Requests.Commands.Walker.UpdateWalkerCommand;
using Application.Requests.Queries.Walker.GetCurrentWalkerQuery;
using Application.Requests.Queries.Walker.GetPageOfWalkersQuery;
using Application.Requests.Queries.Walker.GetWalkerByIdQuery;
using Application.Responses;
using Application.Responses.Walker;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "Walker, Admin")]
[Route("api/[Controller]")]
public class WalkerController : ControllerBase
{
    private readonly IMediator _mediator;

    public WalkerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("CreateWalker")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWalker(CreateWalkerCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpGet("GetWalker/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult<WalkerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWalker(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetWalkerByIdQuery(id), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetCurrentWalker")]
    [ProducesResponseType(typeof(CommonResult<WalkerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCurrentWalker(CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetCurrentWalkerQuery(User.GetUserId()), cancellationToken)).ToActionResult();
    }


    [HttpGet("GetPageOfWalkers")]
    [ProducesResponseType(typeof(CommonResult<PageOf<WalkerResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfWalkers(
        string? nameSearchTerm,
        string? skillSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetPageOfWalkersQuery(
                    nameSearchTerm,
                    skillSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken)).ToActionResult();
    }

    [HttpPut("UpdateWalker")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateWalker(
        UpdateWalkerCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeleteWalker/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWalker(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteWalkerCommand(id), cancellationToken)).ToActionResult();
    }

    [HttpPost("Walker/{id:Guid}/Image")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadWalkerImage(Guid id, IFormFile file, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new UploadImageCommand(id, file), cancellationToken)).ToActionResult();
    }

    [HttpGet("Walker/{id:Guid}/Image")]
    [ProducesResponseType(typeof(CommonResult<FileStreamResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWalkerImage(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetImageCommand(id), cancellationToken);

        return File(result.Data.ResponseStream, result.Data.Headers.ContentType);
    }

    [HttpDelete("Walker/{id:Guid}/Image")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWalkerImage(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteImageCommand(id), cancellationToken)).ToActionResult();
    }
}