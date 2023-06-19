#pragma warning disable CS8618
namespace Doggo.Domain.Entities.User;

using Base;
using Documents;
using DogOwner;
using Microsoft.AspNetCore.Identity;
using Walker;

public class User : IdentityUser<int>, IEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int Age { get; set; }

    public PersonalIdentifier PersonalIdentifier { get; set; }

    public DogOwner DogOwner { get; set; }

    public Walker Walker { get; set; }

    public bool GoogleAuth { get; set; }

    public bool FacebookAuth { get; set; }

    public bool InstagramAuth { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}