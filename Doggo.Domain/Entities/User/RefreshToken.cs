namespace Doggo.Domain.Entities.User;

public class RefreshToken
{
    public required Guid Token { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime Expired  { get; set; }

    public User User { get; set; }
    public Guid UserId { get; set; }
}