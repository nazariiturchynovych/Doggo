namespace Doggo.Application.UnitTests.Requests.Commands.TestUtils;

using Application.Requests.Commands.Job.AcceptJobCommand;
using Application.Requests.Commands.Job.CreateAndApplyJobCommand;
using Application.Requests.Commands.Job.DeleteJobCommand;
using Application.Requests.Commands.Job.DoneJobCommand;
using Application.Requests.Commands.Job.RejectJobCommand;
using Application.Requests.Commands.Job.UpdateJobCommand;
using UnitTests.TestUtils.Constants;

public static class CreateJobCommandUtils
{
    public static CreateAndApplyJobCommand CreateAndApplyJobCommand()
    {
        return new CreateAndApplyJobCommand(
            DogId: Constants.ValidDog.Id,
            DogOwnerId: Guid.NewGuid(),
            JobRequestId: Guid.NewGuid(),
            Comment: Constants.ValidJob.Comment,
            Payment: Constants.ValidJob.Payment);
    }

    public static AcceptJobCommand AcceptJobCommand()
    {
        return new AcceptJobCommand(
            JobId: Constants.ValidJob.Id);
    }

    public static RejectJobCommand RejectJobCommand()
    {
        return new RejectJobCommand(
            JobId: Constants.ValidJob.Id);
    }

    public static DeleteJobCommand DeleteJobCommand()
    {
        return new DeleteJobCommand(
            JobId: Constants.ValidJob.Id);
    }

    public static DoneJobCommand DoneJobCommand()
    {
        return new DoneJobCommand(
            JobId: Constants.ValidJob.Id);
    }
    public static UpdateJobCommand UpdateJobCommand()
    {
        return new UpdateJobCommand(
            JobId: Constants.ValidJob.Id,
            Comment: Constants.ValidJob.Comment,
            Payment: Constants.ValidJob.Payment);
    }
}