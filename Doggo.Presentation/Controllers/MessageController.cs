namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Message.CreateMessageCommand;
using Application.Requests.Commands.Message.DeleteMessageCommand;
using Application.Requests.Commands.Message.UpdateMessageCommand;
using Application.Requests.Queries.Message.GetChatMessagesQuery;
using Application.Requests.Queries.Message.GetUserMessagesQuery;
using Application.Responses.Chat.Message;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMessage(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }


    [HttpGet("GetMessageOwnersMessages")]
    [ProducesResponseType(typeof(CommonResult<List<MessageResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserMessages(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetUserMessagesQuery(dogOwnerId), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetChatMessages")]
    [ProducesResponseType(typeof(CommonResult<List<MessageResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfMessages(
        Guid chatId,
        int count,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetChatMessagesQuery(chatId, count),
                cancellationToken)).ToActionResult();
    }

    [HttpPut("UpdateMessage")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMessage(
        UpdateMessageCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeleteMessage/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMessage(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteMessageCommand(id), cancellationToken)).ToActionResult();
    }

}