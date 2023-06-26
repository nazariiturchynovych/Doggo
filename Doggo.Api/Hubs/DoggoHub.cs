namespace Doggo.Hubs;

using System.Collections.Concurrent;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Chat;
using Extensions;
using Infrastructure.Repositories.Abstractions;
using Microsoft.AspNetCore.SignalR;

public sealed class DoggoHub : Hub
{
    private readonly IChatRepository _chatRepository;
    private static readonly ConcurrentDictionary<Guid, List<Guid>> UserChatConnections = new();
    private static readonly ConcurrentDictionary<Guid, Chat> Chats = new();

    public DoggoHub(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task SendNotification()
    {
        var a = "from hub";
        Console.WriteLine();
        await Clients.All.SendAsync("ReceiveNotification", a);
    }

    public override async Task OnConnectedAsync()
    {
        UserChatConnections.TryAdd(Context.User!.GetUserId(), new List<Guid>());
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
                await Clients.Caller.SendAsync("onError", "OnConnected:" + CommonErrors.EntityDoesNotExist);
                return;
            }

            Chats.TryAdd(chat.Id, chat);

            cachedChat = chat;
        }

        if (cachedChat!.UserChats.All(x => x.UserId != Context.User!.GetUserId()))
        {
            await Clients.Caller.SendAsync("onError", "OnConnected:" + "User is not participant of this chat");
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, cachedChat.Id.ToString());

        UserChatConnections.TryGetValue(Context.User!.GetUserId(), out var userChatConnections);

        userChatConnections!.Add(chatId);
        UserChatConnections.TryUpdate(Context.User!.GetUserId(), userChatConnections, userChatConnections);
    }


    public async Task SendMessage(Guid chatId, string message)
    {
        var result = Chats.TryGetValue(chatId, out var cachedChat);

        if (result)
        {
            await Clients.Caller.SendAsync("onError", "OnConnected:" + CommonErrors.EntityDoesNotExist);
            return;
        }

        if (cachedChat!.UserChats.All(x => x.UserId != Context.User!.GetUserId()))
        {
            await Clients.Caller.SendAsync("onError", "OnConnected:" + "User is not participant of this chat");
            return;
        }

        await Clients.Group(chatId.ToString()).SendAsync(message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        UserChatConnections.TryRemove(Context.User!.GetUserId(), out var connections);

        if (connections is not null && !connections.Any())
        {
            await base.OnDisconnectedAsync(exception);
            return;
        }

        foreach (var connection in connections!)
        {
            await Groups.RemoveFromGroupAsync(connection.ToString(), Context.ConnectionId);
        }

    }
}