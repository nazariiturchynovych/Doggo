namespace Doggo.Application.UnitTests.TestUtils.Factory;

using Domain.Entities.Job;
using Domain.Entities.Walker;
using Domain.Entities.Walker.Schedule;
using static Constants.Constants;


public static partial class Factory
{
    public static class WalkerFactory
    {
        public static Walker CreateWalker()
        {
            return new Walker()
            {
                Id = ValidWalker.Id,
                About = ValidWalker.About,
                Skills = ValidWalker.Skills,
                UserId = ValidUser.Id,
            };
        }

        public static Walker CreateWalkerWithAllIncludes()
        {
            var walker = CreateWalker();
            walker.User = UserFactory.CreateUser();
            walker.Jobs = new List<Job>() {JobFactory.CreateJob()};
            walker.PossibleSchedules = new List<PossibleSchedule>() {PossibleScheduleFactory.CreatePossibleSchedule()};
            return walker;
        }
    }
}