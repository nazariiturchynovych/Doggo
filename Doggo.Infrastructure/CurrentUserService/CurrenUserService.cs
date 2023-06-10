namespace Doggo.Infrastructure.CurrentUserService;

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public class CurrenUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrenUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? GetUserEmail()
    {
        return _contextAccessor?.HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
    }

    public string? GetUserId()
    {
        return _contextAccessor?.HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
    }

    public string? GetUserRole()
    {
        return _contextAccessor?.HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
    }

    public IEnumerable<string>? GetAllUserRoles()
    {
        return _contextAccessor?.HttpContext?.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
    }
}