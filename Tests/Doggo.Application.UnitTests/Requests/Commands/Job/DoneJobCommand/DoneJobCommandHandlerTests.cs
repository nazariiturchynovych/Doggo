namespace Doggo.Application.UnitTests.Requests.Commands.Job.DoneJobCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Application.Requests.Commands.Job.DoneJobCommand;
using Domain.Constants.ErrorConstants;
using Domain.Entities.DogOwner;
using Domain.Entities.Job;
using Domain.Entities.JobRequest;
using FluentAssertions;
using Moq;
using TestUtils;
using UnitTests.TestUtils.Constants;
using UnitTests.TestUtils.Factory;

public class DoneJobCommandHandlerTests
{
    private readonly Mock<IJobRepository> _jobRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IJobRequestRepository> _jobRequestRepositoryMock;
    private readonly Mock<IDogOwnerRepository> _dogOwnerRepositoryMock;

    public DoneJobCommandHandlerTests()
    {
        _jobRepositoryMock = new Mock<IJobRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _jobRequestRepositoryMock = new Mock<IJobRequestRepository>();
        _dogOwnerRepositoryMock = new Mock<IDogOwnerRepository>();
    }

    [Fact]
    public async Task HandleDoneJobCommand_WhenDogOwnerIsNull_ShouldReturnFailureResult()
    {
        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var command = CreateJobCommandUtils.DoneJobCommand();

        var handler = new DoneJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(DogOwnerErrors.DogOwnerDoesNotExist);
    }

    [Fact]
    public async Task HandleDoneJobCommand_WhenJobIsNull_ShouldReturnFailureResult()
    {
        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner());

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var command = CreateJobCommandUtils.DoneJobCommand();

        var handler = new DoneJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobErrors.JobDoesNotExist);
    }

    [Fact]
    public async Task HandleDoneJobCommand_WhenJobRequestIsNull_ShouldReturnFailureResult()
    {
        var command = CreateJobCommandUtils.DoneJobCommand();

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner());

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Job());

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var handler = new DoneJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobRequestErrors.JobRequestDoesNotExist);
    }

    [Fact]
    public async Task HandleDoneJobCommand_WhenCurrentDogOwnerHasNoJobFromRequest_ShouldReturnFailureResult()
    {
        var command = CreateJobCommandUtils.DoneJobCommand();

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner {Jobs = new List<Job>() {new Job() {Id = command.JobId}}});

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Job());

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetJobRequestWithJobsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => new JobRequest());

        var handler = new DoneJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobErrors.CurrenDogOwnerHasNotThisAppliedJob);
    }

    [Fact]
    public async Task HandleDoneJobCommand_WhenRequestIsValid_ShouldReturnSuccessResult()
    {
        var command = CreateJobCommandUtils.DoneJobCommand();

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner {Jobs = new List<Job>() {Factory.JobFactory.CreateJob()}});

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Job() {Id = Constants.ValidJob.Id});

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetJobRequestWithJobsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(
                () => new JobRequest()
                {
                    Id = Constants.ValidJobRequest.Id,
                    Jobs = new List<Job>() {Factory.JobFactory.CreateJob()}
                });

        var handler = new DoneJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        _jobRepositoryMock.Verify(
            x => x.Update(
                It.Is<Job>(
                    xx =>
                        xx.Id == command.JobId && xx.IsDone == true)),
            Times.Once);

        _jobRequestRepositoryMock.Verify(
            x => x.Remove(It.IsAny<JobRequest>()),
            Times.Once);

        result.IsSuccess.Should().BeTrue();
        result.ErrorMessage.Should().Be(default);
    }
}