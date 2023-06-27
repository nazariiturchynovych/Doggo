namespace Doggo.Api.Application.Mappers;

using Domain.DTO;
using Domain.DTO.Chat.Message;
using Domain.Entities.Chat;
using Requests.Commands.Message;

public static class MessageMapper
{
    public static GetMessageDto MapMessageToGetMessageDto(this Message message)
    {
        return new GetMessageDto(
            message.Value,
            message.UserId,
            message.User.FirstName,
            message.CreatedDate,
            message.ChangedDate);
    }

    public static Message MapMessageUpdateCommandToMessage(this UpdateMessageCommand command, Message message)
    {
        message.Value = command.Value;
        message.ChangedDate = DateTime.UtcNow;
        return message;
    }

    public static PageOfTDataDto<GetMessageDto> MapMessageCollectionToPageOfMessageDto(
        this IReadOnlyCollection<Message> collection)
    {
        var collectionDto = collection.Select(message => message.MapMessageToGetMessageDto()).ToList();

        return new PageOfTDataDto<GetMessageDto>(collectionDto);
    }
}