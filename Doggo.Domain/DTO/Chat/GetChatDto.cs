namespace Doggo.Domain.DTO.Chat;

using Entities.Chat;
using Message;

public record GetChatDto(Guid ChatId, string Name, ICollection<GetMessageDto> Messages);