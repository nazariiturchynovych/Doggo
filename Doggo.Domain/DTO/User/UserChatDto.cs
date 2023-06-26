namespace Doggo.Domain.DTO.User;

public record UserChatDto(Guid UserId, string UserName, string? CurrentRoom = default);