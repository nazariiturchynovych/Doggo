namespace Doggo.Presentation.Controllers;

using Api.Application.Requests.Commands.Message;
using Api.Application.Requests.Queries.Message;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "DogOwner, Admin, Walker")]
[Route("api/[Controller]")]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateMessage")]
    public async Task<IActionResult> CreateMessage(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }


    [HttpGet("GetMessageOwnersMessages")]
    public async Task<IActionResult> GetUserMessages(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserMessagesQuery(dogOwnerId), cancellationToken));
    }

    [HttpGet("GetChatMessages")]
    public async Task<IActionResult> GetPageOfMessages(
        Guid chatId,
        int count,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetChatMessagesQuery(chatId, count),
                cancellationToken));
    }

    [HttpPut("UpdateMessage")]
    public async Task<IActionResult> UpdateMessage(
        UpdateMessageCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteMessage/{id:Guid}")]
    public async Task<IActionResult> DeleteMessage(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteMessageCommand(id), cancellationToken));
    }

}