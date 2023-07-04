namespace Doggo.Application.Mappers;

using Domain.Entities.Chat;
using DTO;
using DTO.Chat;
using DTO.Chat.Message;
using DTO.UserChat;
using Requests.Commands.Chat;
using Requests.Commands.Chat.UpdateChatCommand;

public static class ChatMapper
{
    public static Chat MapUpdateChatCommandToChat(this UpdateChatCommand command, Chat chat)
    {
        chat.Name = command.Name ?? chat.Name;
        return chat;
    }

    public static GetChatDto MapChatToGetChatDto(this Chat chat)
    {
        return new GetChatDto(
            chat.Id,
            chat.Name,
            chat.Messages is not null ? chat.Messages.Select(x => x.MapMessageToGetMessageDto()).ToList() : new List<GetMessageDto>(),
            chat.UserChats is not null ? chat.UserChats.Select(x => x.MapUserChatToUserChatDto()).ToList() : new List<UserChatDto>());
    }

    public static ChatDto MapChatToChatDto(this Chat chat)
    {
        return new ChatDto(chat.Id, chat.Name);
    }

    public static PageOfTDataDto<ChatDto> MapChatCollectionToPageOfChatDto(this IReadOnlyCollection<Chat> collection)
    {
        var collectionDto = collection.Select(dogOwner => dogOwner.MapChatToChatDto()).ToList();

        return new PageOfTDataDto<ChatDto>(collectionDto);
    }

    public static UserChatDto MapUserChatToUserChatDto(this UserChat userChat)
    {
        return new UserChatDto
        {
            ChatId = userChat.ChatId,
            UserId = userChat.UserId
        };
    }
}