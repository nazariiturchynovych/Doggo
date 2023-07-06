namespace Doggo.Application.UnitTests.TestUtils.Factory;

using Domain.Entities.User;
using static Constants.Constants;


public static partial class Factory
{
    public static class UserFactory
    {
        public static User CreateUser()
        {
            return new User()
            {
                Id = ValidUser.Id,
                FirstName = ValidUser.FirstName,
                LastName = ValidUser.LastName,
                IsApproved = ValidUser.IsApproved,
                Age = ValidUser.Age,
                Email = ValidUser.Email,
                EmailConfirmed = ValidUser.EmailConfirmed,
            };
        }
    }


}