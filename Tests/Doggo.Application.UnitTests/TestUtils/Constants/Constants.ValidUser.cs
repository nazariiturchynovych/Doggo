namespace Doggo.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class ValidUser
    {
        public static readonly Guid Id = new ("11111111-1111-1111-1111-111111111111");

        public const string FirstName = "FirstName";

        public const string LastName = "LastName";

        public const bool IsApproved = true;

        public const int Age = 18;

        public const string Email = "user@gmail.com";

        public const bool EmailConfirmed = true;
    }
}