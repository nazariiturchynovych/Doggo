namespace Doggo.Infrastructure.Services.CurrentUserService;

public interface ICurrentUserService
{
    public string GetUserEmail();

    public int GetUserId();

    public string GetUserRole();

    public IEnumerable<string> GetAllUserRoles();
}