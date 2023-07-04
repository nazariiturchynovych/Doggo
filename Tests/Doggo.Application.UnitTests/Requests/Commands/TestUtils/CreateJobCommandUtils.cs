namespace Doggo.Application.UnitTests.Requests.Commands.TestUtils;

using Application.Requests.Commands.Job.CreateAndApplyJobCommand;
using UnitTests.TestUtils;

public static class CreateJobCommandUtils
{
    public static CreateAndApplyJobCommand CreateAndApplyJobCommand()
    {
        return new CreateAndApplyJobCommand(
            DogId: Guid.NewGuid(),
            DogOwnerId: Guid.NewGuid(),
            JobRequestId: Guid.NewGuid(),
            Comment: Constants.Job.Comment,
            Payment: Constants.Job.Payment);
    }
}