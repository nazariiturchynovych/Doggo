namespace Doggo.Domain.Entities.User;

using Enums;
using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole<int>
{
    public RoleType RoleType { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();
}