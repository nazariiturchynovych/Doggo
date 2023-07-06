namespace Doggo.Application.UnitTests.TestUtils.Constants;

using Domain.Enums;

public static partial class Constants
{
    public static class ValidJob
    {
        public static readonly Guid Id = new("88888888-8888-8888-8888-888888888888");

        public static readonly DateTime CreatedDate = DateTime.UtcNow;
        
        public const string Comment = "Some comment to test";
        
        public const JobStatus Status = JobStatus.Applied;

        public const decimal Payment = 50;
    }
}