namespace Doggo.Infrastructure.Services.CurrentUserService;

public interface ICurrentUserService
{
    public string GetUserEmail();

    public Guid GetUserId();

    public string GetUserRole();

    public IEnumerable<string> GetAllUserRoles();
}