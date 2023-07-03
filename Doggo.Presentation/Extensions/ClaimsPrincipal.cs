namespace Doggo.Presentation.Extensions;

using System.Security.Claims;

public static class ClaimsPrincipal
{
    public static Guid GetUserId(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

       return claim is not null ? Guid.Parse(claim.Value) : throw new Exception("User is not logged in or does not have claims");

    }
}