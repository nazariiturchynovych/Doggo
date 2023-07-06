namespace Doggo.Application.UnitTests.Requests.Commands.Job.DeleteJobCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Application.Requests.Commands.Job.DeleteJobCommand;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Job;
using Domain.Entities.Walker;
using FluentAssertions;
using Infrastructure.Services.CurrentUserService;
using Moq;
using TestUtils;
using UnitTests.TestUtils.Factory;

public class DeleteJobCommandHandlerTests
{
    private readonly Mock<IJobRepository> _jobRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IWalkerRepository> _walkerRepositoryMock;

    public DeleteJobCommandHandlerTests()
    {
        _jobRepositoryMock = new Mock<IJobRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _walkerRepositoryMock = new Mock<IWalkerRepository>();
    }

    [Fact]
    public async Task HandleDeleteJobCommand_WhenWalkerIsNull_ShouldReturnFailureResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var command = CreateJobCommandUtils.DeleteJobCommand();

        var handler = new DeleteJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(WalkerErrors.WalkerDoesNotExist);
    }

    [Fact]
    public async Task HandleDeleteJobCommand_WhenJobIsNull_ShouldReturnFailureResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Factory.WalkerFactory.CreateWalker);

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var command = CreateJobCommandUtils.DeleteJobCommand();

        var handler = new DeleteJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobErrors.JobDoesNotExist);
    }

    [Fact]
    public async Task HandleDeleteJobCommand_WhenCurrentWalkerIsNOtOwnerOfJobFromRequest_ShouldReturnFailureResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker());

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Factory.JobFactory.CreateJob);

        _jobRepositoryMock.Setup(
                x =>
                    x.GetWalkerJobsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new List<Job>());

        var command = CreateJobCommandUtils.DeleteJobCommand();

        var handler = new DeleteJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(WalkerErrors.WalkerIsNotOwnerOfThisJob);
    }

    [Fact]
    public async Task HandleDeleteJobCommand_WhenRequestIsValid_ShouldReturnSuccessResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker());

        _jobRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Factory.JobFactory.CreateJob);

        _jobRepositoryMock.Setup(
                x =>
                    x.GetWalkerJobsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new List<Job>() {Factory.JobFactory.CreateJob()});

        var command = CreateJobCommandUtils.DeleteJobCommand();

        var handler = new DeleteJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        _jobRepositoryMock.Verify(
            x => x.Remove(
                It.Is<Job>(
                    xx =>
                        xx.Id == command.JobId)),
            Times.Once);

        result.IsSuccess.Should().BeTrue();
        result.ErrorMessage.Should().Be(default);
    }
}