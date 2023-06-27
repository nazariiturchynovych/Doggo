namespace Doggo.Api.Application.Mappers;

using Domain.DTO;
using Domain.DTO.Chat;
using Domain.Entities.Chat;
using Requests.Commands.Chat;

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
            chat.Messages.Select(x => x.MapMessageToGetMessageDto())
                .ToList());
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
}