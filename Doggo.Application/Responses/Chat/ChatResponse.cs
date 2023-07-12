namespace Doggo.Application.Responses.Chat;

using Message;
using UserChat;

public record ChatResponse(Guid ChatId, string Name, ICollection<MessageResponse> Messages, ICollection<UserChatResponse> UserChats);