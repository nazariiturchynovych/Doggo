namespace Doggo.Domain.DTO.User;

using Entities.Chat;

public class UserChats
{
    public bool WereUpdated { get; set; }

    public List<Chat> Chats { get; set; }
}