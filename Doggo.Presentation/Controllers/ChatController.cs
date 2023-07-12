namespace Doggo.Presentation.Controllers;

using Application.Requests.Commands.Chat.AddUsersToChatCommand;
using Application.Requests.Commands.Chat.CreateGroupChatCommand;
using Application.Requests.Commands.Chat.CreatePrivateChatCommand;
using Application.Requests.Commands.Chat.DeleteChatCommand;
using Application.Requests.Commands.Chat.DeleteUsersFromChatCommand;
using Application.Requests.Commands.Chat.UpdateChatCommand;
using Application.Requests.Queries.Chat.GetChatByIdQuery;
using Application.Requests.Queries.Chat.GetPageOfChatsQuery;
using Application.Requests.Queries.Chat.GetUserChatsQuery;
using Application.Responses;
using Application.Responses.Chat;
using Domain.Results;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePrivateChat(CreatePrivateChatCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPost("CreateGroupChat")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGroupChat(CreateGroupChatCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPost("AddUsersToChat")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddUsersToChat(AddUsersToChatCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpPost("DeleteUsersFromChat")]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUsersFromChat(DeleteUsersFromChatCommand command, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpGet("GetChat/{id:Guid}")]
    [ProducesResponseType(typeof(CommonResult<ChatDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetChat(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetChatByIdQuery(id), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetUserChats")]
    [ProducesResponseType(typeof(CommonResult<PageOf<ChatResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserChats(
        Guid dogOwnerId,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new GetUserChatsQuery(dogOwnerId), cancellationToken)).ToActionResult();
    }

    [HttpGet("GetPageOfChats")]
    [ProducesResponseType(typeof(CommonResult<PageOf<ChatResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommonResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPageOfChats(
        string? nameSearchTerm,
        string? sortColumn,
        string? sortOrder,
        int pageCount,
        int page,
        CancellationToken cancellationToken)
    {
        return (
            await _mediator.Send(
                new GetPageOfChatsQuery(
                    nameSearchTerm,
                    sortColumn,
                    sortOrder,
                    page,
                    pageCount),
                cancellationToken)).ToActionResult();
    }

    [HttpPut("UpdateChat")]
    public async Task<IActionResult> UpdateChat(
        UpdateChatCommand command,
        CancellationToken cancellationToken)
    {
        return (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }

    [HttpDelete("DeleteChat/{id:Guid}")]
    public async Task<IActionResult> DeleteChat(Guid id, CancellationToken cancellationToken)
    {
        return (await _mediator.Send(new DeleteChatCommand(id), cancellationToken)).ToActionResult();
    }

}