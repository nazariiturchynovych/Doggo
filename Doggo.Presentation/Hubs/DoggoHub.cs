namespace Doggo.Presentation.Hubs;

using System.Collections.Concurrent;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Mappers;
using Application.Requests.Commands.Message.CreateMessageCommand;
using Application.Responses.Chat;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Chat;
using MediatR;
using Microsoft.AspNetCore.SignalR;

public sealed class DoggoHub : Hub<IDoggoHub>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMediator _mediator;
    private readonly ICacheService _cacheService;
    private static readonly ConcurrentDictionary<Guid, List<Guid>> UserChatConnections = new();
    private readonly Guid _userId;

    public DoggoHub(IChatRepository chatRepository, IMediator mediator, ICacheService cacheService, ICurrentUserService currentUserService)
    {
        _chatRepository = chatRepository;
        _mediator = mediator;
        _cacheService = cacheService;
        _userId = currentUserService.GetUserId();
    }


    public override async Task OnConnectedAsync()
    {
        UserChatConnections.TryAdd(_userId, new List<Guid>());
        await base.OnConnectedAsync();
    }

    public async Task JoinChat(Guid chatId)
    {
        var cachedChat = await _cacheService.GetData<ChatResponse>(CacheKeys.Chat + chatId);

        if (cachedChat is null)
        {
            var chat = await _chatRepository.GetAsync(chatId);

            if (chat is null)
            {
                await Clients.Caller.OnError(CommonErrors.EntityDoesNotExist);
                return;
            }

            cachedChat = chat.MapChatToChatResponse();

            await _cacheService.SetData(CacheKeys.Chat + chat.Id, cachedChat);

        }

        await Groups.AddToGroupAsync(Context.ConnectionId, cachedChat.ChatId.ToString());

        UserChatConnections.TryGetValue(_userId, out var userChatConnections);

        userChatConnections!.Add(chatId);

        UserChatConnections.TryUpdate(_userId, userChatConnections, userChatConnections);
    }


    public async Task SendMessage(Guid chatId, string message)
    {
        var cachedChat = await _cacheService.GetData<ChatResponse>(CacheKeys.Chat + chatId);

        if (cachedChat is null)
        {
            await Clients.Caller.OnError(CommonErrors.EntityDoesNotExist);
            return;
        }

        if (cachedChat.UserChats.All(x => x.UserId != _userId))
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

    public static async Task UpdateChat(Guid chatToUpdateId, ChatResponse chat, ICacheService cacheService)
    {
      var chatToUpdate =  await cacheService.GetData<Chat>(CacheKeys.Chat + chatToUpdateId);
        if (chatToUpdate is not null)
        {
           await cacheService.SetData(CacheKeys.Chat + chatToUpdateId, chat);
        }
    }

}