namespace Doggo.Application.UnitTests.Requests.Commands.Job.RejectJobCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Application.Requests.Commands.Job.RejectJobCommand;
using Domain.Constants.ErrorConstants;
using Domain.Entities.DogOwner;
using Domain.Entities.Job;
using Domain.Enums;
using FluentAssertions;
using Moq;
using TestUtils;
using UnitTests.TestUtils.Constants;
using UnitTests.TestUtils.Factory;

public class RejectJobCommandHandlerTests
{
    private readonly Mock<IJobRepository> _jobRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IDogOwnerRepository> _dogOwnerRepositoryMock;

    public RejectJobCommandHandlerTests()
    {
        _jobRepositoryMock = new Mock<IJobRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _dogOwnerRepositoryMock = new Mock<IDogOwnerRepository>();
    }

    [Fact]
    public async Task HandleRejectJobCommand_WhenDogOwnerIsNull_ShouldReturnFailureResult()
    {
        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var command = CreateJobCommandUtils.RejectJobCommand();

        var handler = new RejectJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(DogOwnerErrors.DogOwnerDoesNotExist);
    }

    [Fact]
    public async Task HandleRejectJobCommand_WhenJobIsNull_ShouldReturnFailureResult()
    {
        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner());

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var command = CreateJobCommandUtils.RejectJobCommand();

        var handler = new RejectJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobErrors.JobDoesNotExist);
    }

    [Fact]
    public async Task HandleRejectJobCommand_WhenCurrentDogOwnerHasNoJobFromRequest_ShouldReturnFailureResult()
    {
        var command = CreateJobCommandUtils.RejectJobCommand();

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner {Jobs = new List<Job>() {new Job() {Id = command.JobId}}});

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Job());


        var handler = new RejectJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

            var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobErrors.CurrenDogOwnerHasNotThisAppliedJob);
    }


    [Fact]
    public async Task HandleRejectJobCommand_WhenRequestIsValid_ShouldReturnSuccessResult()
    {
        var command = CreateJobCommandUtils.RejectJobCommand();

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetWithJobRequestAndJobsAsyncByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner {Jobs = new List<Job>() {Factory.JobFactory.CreateJob()}});

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Job(){Id = Constants.ValidJob.Id});


        var handler = new RejectJobCommandHandler(
            _currentUserServiceMock.Object,
            _jobRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        _jobRepositoryMock.Verify(
            x => x.Update(
                It.Is<Job>(
                    xx =>
                        xx.Id == command.JobId && xx.Status == JobStatus.Rejected)),
            Times.Once);

        result.IsSuccess.Should().BeTrue();
        result.ErrorMessage.Should().Be(default);
    }
}