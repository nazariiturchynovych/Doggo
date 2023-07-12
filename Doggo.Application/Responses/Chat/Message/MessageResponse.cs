namespace Doggo.Application.Responses.Chat.Message;

public record MessageResponse(
    string Value,
    Guid UserId,
    string UserName,
    DateTime CreateDate,
    DateTime? ChangedDate);