namespace Doggo.Application.UnitTests.TestUtils.Constants;

public static partial class Constants
{
    public static class ValidPossibleSchedule
    {
        public static readonly Guid Id = new ("77777777-7777-7777-7777-777777777777");

        public static readonly DateTime From = DateTime.UtcNow.AddDays(7);

        public static readonly DateTime To = From.AddHours(2);

    }
}