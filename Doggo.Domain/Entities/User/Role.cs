namespace Doggo.Domain.Entities.User;

using Microsoft.AspNetCore.Identity;

public sealed class Role : IdentityRole<Guid>
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