namespace Doggo.Application.DTO.Chat;

using Message;
using UserChat;

public record GetChatDto(Guid ChatId, string Name, ICollection<GetMessageDto> Messages, ICollection<UserChatDto> UserChats);