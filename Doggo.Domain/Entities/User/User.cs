#pragma warning disable CS8618
namespace Doggo.Domain.Entities.User;

using Base;
using Chat;
using Documents;
using DogOwner;
using Microsoft.AspNetCore.Identity;
using Walker;

public class User : IdentityUser<Guid>, IEntity
{

    public User()
    {

    }
    public User(
        Guid id,
        string first_name,
        string last_name,
        int age,
        string phone_number)
    {
        Id = id;
        FirstName = first_name;
        LastName = last_name;
        Age = age;
        PhoneNumber = phone_number;
    }

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

    public ICollection<UserChat> UserChats { get; set; }

    public ICollection<Message> Messages { get; set; }
}