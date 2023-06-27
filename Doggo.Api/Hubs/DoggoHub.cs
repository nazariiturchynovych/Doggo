namespace Doggo.Api.Hubs;

using System.Collections.Concurrent;
using Application.Requests.Commands.Message;
using Doggo.Extensions;
using Doggo.Hubs;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Chat;
using Infrastructure.Repositories.Abstractions;
using MediatR;
using Microsoft.AspNetCore.SignalR;

public sealed class DoggoHub : Hub<IDoggoHub>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMediator _mediator;
    private static readonly ConcurrentDictionary<Guid, List<Guid>> UserChatConnections = new();
    public static readonly ConcurrentDictionary<Guid, Chat> Chats = new();
    private readonly Guid _userId;

    public DoggoHub(IChatRepository chatRepository, IMediator mediator)
    {
        _chatRepository = chatRepository;
        _mediator = mediator;
        _userId = Context.User!.GetUserId();
    }


    public override async Task OnConnectedAsync()
    {
        UserChatConnections.TryAdd(_userId, new List<Guid>());
        await base.OnConnectedAsync();
    }

    public async Task JoinChat(Guid chatId)
    {
        var result = Chats.TryGetValue(chatId, out var cachedChat);

        if (!result)
        {
            var chat = await _chatRepository.GetAsync(chatId);

            if (chat is null)
            {
                await Clients.Caller.OnError(CommonErrors.EntityDoesNotExist);
                return;
            }

            Chats.TryAdd(chat.Id, chat);

            cachedChat = chat;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, cachedChat!.Id.ToString());

        UserChatConnections.TryGetValue(_userId, out var userChatConnections);

        userChatConnections!.Add(chatId);

        UserChatConnections.TryUpdate(_userId, userChatConnections, userChatConnections);
    }


    public async Task SendMessage(Guid chatId, string message)
    {
        var result = Chats.TryGetValue(chatId, out var cachedChat);

        if (result)
        {
            await Clients.Caller.OnError(CommonErrors.EntityDoesNotExist);
            return;
        }

        if (cachedChat!.UserChats.All(x => x.UserId != _userId))
        {
            await Clients.Caller.OnError("User is not participant of this chat");
            return;
        }

        await _mediator.Send(new CreateMessageCommand(_userId, chatId, message));

        await Clients.Group(chatId.ToString()).ReceiveMessage(message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        UserChatConnections.TryRemove(_userId, out var connections);

        if (connections is null || !connections.Any())
        {
            await base.OnDisconnectedAsync(exception);
            return;
        }

        foreach (var connection in connections)
        {
            await Groups.RemoveFromGroupAsync(connection.ToString(), Context.ConnectionId);
        }
    }

    public static void UpdateChat(Guid chatToUpdateId, Chat chat)
    {
        Chats.TryGetValue(chatToUpdateId, out var chatToUpdate);
        if (chatToUpdate is not null)
        {
            Chats.TryUpdate(chatToUpdateId, chat, chatToUpdate);
        }
    }
}