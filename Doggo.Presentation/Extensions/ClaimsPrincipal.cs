namespace Doggo.Presentation.Extensions;

using System.Security.Claims;

public static class ClaimsPrincipal
{
    public static Guid GetUserId(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
    {
        return Guid.Parse(claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}