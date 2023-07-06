namespace Doggo.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class ValidRequiredSchedule
    {
        public static readonly Guid Id = new ("66666666-6666-6666-6666-666666666666");

        public static readonly DateTime From = DateTime.UtcNow.AddDays(6);

        public static readonly DateTime To = From.AddHours(2);

        public const bool IsRegular = true;
    }
}