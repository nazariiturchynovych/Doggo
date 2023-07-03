#pragma warning disable CS8618
namespace Doggo.Application.DTO.User;

using Doggo.Domain.Entities.Chat;

public class UserChats
{
    public bool WereUpdated { get; set; }

    public List<Chat> Chats { get; set; }
}