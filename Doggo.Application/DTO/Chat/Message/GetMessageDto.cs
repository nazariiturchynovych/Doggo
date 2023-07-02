namespace Doggo.Domain.DTO.Chat.Message;

public record GetMessageDto(
    string Value,
    Guid UserId,
    string UserName,
    DateTime CreateDate,
    DateTime? ChangedDate);