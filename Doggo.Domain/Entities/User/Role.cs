namespace Doggo.Domain.Entities.User;

using Enums;
using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole<Guid>
{

    public Role()
    {

    }
    public Role(string roleName) : this()
    {
        Name = roleName;
    }

    public ICollection<UserRole> UserRoles { get; set; }
        = new List<UserRole>();
}