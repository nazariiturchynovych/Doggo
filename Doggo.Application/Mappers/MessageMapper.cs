namespace Doggo.Application.Mappers;

using Domain.Entities.Chat;
using Requests.Commands.Message.UpdateMessageCommand;
using Responses;
using Responses.Chat.Message;

public static class MessageMapper
{
    public static MessageResponse MapMessageToMessageResponse(this Message message)
    {
        return new MessageResponse(
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

    public static PageOf<MessageResponse> MapMessageCollectionToPageOfMessageResponse(
        this IReadOnlyCollection<Message> collection)
    {
        var collectionDto = collection.Select(message => message.MapMessageToMessageResponse()).ToList();

        return new PageOf<MessageResponse>(collectionDto);
    }

    public static List<MessageResponse> MapMessageCollectionToListOfMessageResponse(
        this IReadOnlyCollection<Message> collection)
    {
        return collection.Select(message => message.MapMessageToMessageResponse()).ToList();
    }
}