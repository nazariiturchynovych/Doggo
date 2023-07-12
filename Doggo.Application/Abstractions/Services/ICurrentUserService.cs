namespace Doggo.Application.Abstractions.Services;

public interface ICurrentUserService
{
    public string GetUserEmail();

    public Guid GetUserId();

    public string GetUserRole();

    public IEnumerable<string> GetAllUserRoles();
}