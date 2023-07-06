namespace Doggo.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class ValidJobRequest
    {
        public static readonly Guid Id = new("55555555-5555-5555-5555-555555555555");

        public static readonly DateTime CreatedDate = DateTime.UtcNow;

        public const int RequiredAge = 12;

        public const bool HasAcceptedJob = false;

        public const bool IsPersonalIdentifierRequired = false;

        public const decimal PaymentTo = 100;

        public const string Description = "Valid description";

    }
}