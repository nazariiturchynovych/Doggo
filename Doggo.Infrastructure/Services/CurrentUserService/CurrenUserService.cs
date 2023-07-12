namespace Doggo.Infrastructure.Services.CurrentUserService;

using System.Security.Claims;
using Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;

public class CurrenUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    private readonly Exception _exception = new Exception("Something goes wrong while trying get user claims");

    public CurrenUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetUserEmail()
    {
        return _contextAccessor?.HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.Email).Value ?? throw _exception;
    }

    public Guid GetUserId()
    {
        var id = _contextAccessor?.HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value ?? throw _exception;
        return Guid.Parse(id);
    }

    public string GetUserRole()
    {
        return _contextAccessor?.HttpContext?.User.Claims.First(x => x.Type == ClaimTypes.Role).Value ?? throw _exception;
    }

    public IEnumerable<string> GetAllUserRoles()
    {
        return _contextAccessor?.HttpContext?.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value) ?? throw _exception;
    }
}