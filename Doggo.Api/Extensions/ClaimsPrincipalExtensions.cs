namespace Doggo.Extensions;

using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return int.Parse(claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}