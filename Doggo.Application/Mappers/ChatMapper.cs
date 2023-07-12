namespace Doggo.Application.Mappers;

using Domain.Entities.Chat;
using Requests.Commands.Chat.UpdateChatCommand;
using Responses;
using Responses.Chat;
using Responses.Chat.Message;
using Responses.UserChat;

public static class ChatMapper
{
    public static Chat MapUpdateChatCommandToChat(this UpdateChatCommand command, Chat chat)
    {
        chat.Name = command.Name ?? chat.Name;
        return chat;
    }

    public static ChatResponse MapChatToChatResponse(this Chat chat)
    {
        return new ChatResponse(
            chat.Id,
            chat.Name,
            chat.Messages is not null
                ? chat.Messages.Select(x => x.MapMessageToMessageResponse()).ToList()
                : new List<MessageResponse>(),
            chat.UserChats is not null
                ? chat.UserChats.Select(x => x.MapUserChatToUserChatResponse()).ToList()
                : new List<UserChatResponse>());
    }


    public static PageOf<ChatResponse> MapChatCollectionToPageOfChatResponse(
        this IReadOnlyCollection<Chat> collection)
    {
        var collectionDto = collection.Select(dogOwner => dogOwner.MapChatToChatResponse()).ToList();

        return new PageOf<ChatResponse>(collectionDto);
    }

    public static List<ChatResponse> MapChatCollectionToListOfChatResponse(this IReadOnlyCollection<Chat> collection)
    {
        return collection.Select(dogOwner => dogOwner.MapChatToChatResponse()).ToList();
    }

    public static UserChatResponse MapUserChatToUserChatResponse(this UserChat userChat)
    {
        return new UserChatResponse
        {
            ChatId = userChat.ChatId,
            UserId = userChat.UserId
        };
    }
}