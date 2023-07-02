namespace Doggo.Presentation.Controllers;

using Api.Application.Requests.Commands.Chat;
using Application.Requests.Commands.Chat;
using Application.Requests.Queries.Chat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize(Roles = "User")]
[Route("api/[Controller]")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreatePrivateChat")]
    public async Task<IActionResult> CreatePrivateChat(CreatePrivateChatCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPost("CreateGroupChat")]
    public async Task<IActionResult> CreateGroupChat(CreateGroupChatCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPost("AddUsersToChat")]
    public async Task<IActionResult> AddUsersToChat(AddUsersToChatCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpPost("DeleteUsersFromChat")]
    public async Task<IActionResult> DeleteUsersFromChat(DeleteUsersFromChatCommand command, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet("GetChat/{id:Guid}")]
    public async Task<IActionResult> GetChat(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetChatByIdQuery(id), cancellationToken));
    }

    [HttpGet("GetUserChats")]
    public async Task<IActionResult> GetUserChats(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserChatsQuery(dogOwnerId), cancellationToken));
    }

    [HttpGet("GetPageOfChats")]
    public async Task<IActionResult> GetPageOfChats(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return Ok(
            await _mediator.Send(
                new GetPageOfChatsQuery(
                    nameSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken));
    }

    [HttpPut("UpdateChat")]
    public async Task<IActionResult> UpdateChat(
        UpdateChatCommand command,
        CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }

    [HttpDelete("DeleteChat/{id:Guid}")]
    public async Task<IActionResult> DeleteChat(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new DeleteChatCommand(id), cancellationToken));
    }

}