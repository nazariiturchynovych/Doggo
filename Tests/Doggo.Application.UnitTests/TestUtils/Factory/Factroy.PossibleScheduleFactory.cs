namespace Doggo.Application.UnitTests.TestUtils.Factory;

using Domain.Entities.Walker.Schedule;
using static Constants.Constants;


public static partial class Factory
{
    public static class PossibleScheduleFactory
    {
        public static PossibleSchedule CreatePossibleSchedule()
        {
            return new PossibleSchedule()
            {
                Id = ValidPossibleSchedule.Id,
                From = ValidPossibleSchedule.From,
                To = ValidPossibleSchedule.To,
                WalkerId = ValidWalker.Id
            };
        }
    }
}