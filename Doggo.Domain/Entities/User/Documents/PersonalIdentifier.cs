#pragma warning disable CS8618
namespace Doggo.Domain.Entities.User.Documents;

using Base;
using Enums;

public class PersonalIdentifier : Entity
{
    public PersonalIdentifierType PersonalIdentifierType { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }
}