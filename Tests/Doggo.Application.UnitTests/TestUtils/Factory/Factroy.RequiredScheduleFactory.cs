namespace Doggo.Application.UnitTests.TestUtils.Factory;

using Domain.Entities.JobRequest.Schedule;
using static Constants.Constants;


public static partial class Factory
{
    public static class RequiredScheduleFactory
    {
        public static RequiredSchedule CreateRequiredSchedule()
        {
            return new RequiredSchedule()
            {
                Id = ValidRequiredSchedule.Id,
                From = ValidRequiredSchedule.From,
                To = ValidRequiredSchedule.To,
                JobRequestId = ValidJobRequest.Id,
            };
        }
    }
}