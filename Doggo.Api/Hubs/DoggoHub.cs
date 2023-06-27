namespace Doggo.Hubs;

using System.Collections.Concurrent;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Chat;
using Extensions;
using Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.SignalR;

public sealed class DoggoHub : Hub<IDoggoHub>
{
    private readonly IChatRepository _chatRepository;
    private static readonly ConcurrentDictionary<Guid, List<Guid>> UserChatConnections = new();
    private static readonly ConcurrentDictionary<Guid, Chat> Chats = new();
    private readonly Guid _userId;

    public DoggoHub(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
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
            var chat = await _chatRepository.GetByIdAsync(chatId);

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
}