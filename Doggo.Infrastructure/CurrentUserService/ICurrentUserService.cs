namespace Doggo.Infrastructure.CurrentUserService;

public interface ICurrentUserService
{
    public string? GetUserEmail();

    public string? GetUserId();

    public string? GetUserRole();

    public IEnumerable<string>? GetAllUserRoles();
}