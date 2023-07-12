namespace Doggo.Application.UnitTests.Requests.Commands.Job.CreateAndApplyJobCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Application.Requests.Commands.Job.CreateAndApplyJobCommand;
using Domain.Constants.ErrorConstants;
using Domain.Entities.DogOwner;
using Domain.Entities.Job;
using Domain.Entities.JobRequest;
using Domain.Entities.Walker;
using Domain.Enums;
using FluentAssertions;
using Moq;
using TestUtils;
using UnitTests.TestUtils.Constants;

public class CreateAndApplyJobCommandHandlerTests
{
    private readonly Mock<IJobRepository> _jobRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly Mock<IWalkerRepository> _walkerRepositoryMock;
    private readonly Mock<IJobRequestRepository> _jobRequestRepositoryMock;
    private readonly Mock<IDogOwnerRepository> _dogOwnerRepositoryMock;

    public CreateAndApplyJobCommandHandlerTests()
    {
        _jobRepositoryMock = new Mock<IJobRepository>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _walkerRepositoryMock = new Mock<IWalkerRepository>();
        _jobRequestRepositoryMock = new Mock<IJobRequestRepository>();
        _dogOwnerRepositoryMock = new Mock<IDogOwnerRepository>();
    }

    [Fact]
    public async Task HandleCreateAndApplyJobCommand_WhenWalkerIsNull_ShouldReturnFailureResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null);

        var command = CreateJobCommandUtils.CreateAndApplyJobCommand();

        var handler = new CreateAndApplyJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(WalkerErrors.WalkerDoesNotExist);
    }

    [Fact]
    public async Task HandleCreateAndApplyJobCommand_WhenDogOwnerIsNull_ShouldReturnFailureResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker());

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var command = CreateJobCommandUtils.CreateAndApplyJobCommand();

        var handler = new CreateAndApplyJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(DogOwnerErrors.DogOwnerDoesNotExist);
    }

    [Fact]
    public async Task HandleCreateAndApplyJobCommand_WhenCurrentWalkerApplyForHisOwnJob_ShouldReturnFailureResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker());

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner() {UserId = Constants.ValidUser.Id});

        _currentUserServiceMock.Setup(
                x =>
                    x.GetUserId())
            .Returns(Constants.ValidUser.Id);

        var command = CreateJobCommandUtils.CreateAndApplyJobCommand();

        var handler = new CreateAndApplyJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(WalkerErrors.WalkerCanNotApplyJobForHisOwnJobRequest);
    }


    [Fact]
    public async Task HandleCreateAndApplyJobCommand_WhenJobRequestIsNull_ShouldReturnFailureResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker());

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner() {UserId = Constants.ValidUser.Id});

        _currentUserServiceMock.Setup(
                x =>
                    x.GetUserId())
            .Returns(Guid.NewGuid);

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => default);

        var command = CreateJobCommandUtils.CreateAndApplyJobCommand();

        var handler = new CreateAndApplyJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobRequestErrors.JobRequestDoesNotExist);
    }

    [Fact]
    public async Task HandleCreateAndApplyJobCommand_WhenDogOwnerIsNotOwnerOfJobRequest_ShouldReturnFailureResult()
    {
        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker());

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner());

        _currentUserServiceMock.Setup(
                x =>
                    x.GetUserId())
            .Returns(Guid.NewGuid);

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new JobRequest());

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetDogOwnerJobRequests(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<JobRequest>());

        var command = CreateJobCommandUtils.CreateAndApplyJobCommand();

        var handler = new CreateAndApplyJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobRequestErrors.CurrentDogOwnerIsNotOwnerOfThisJobRequest);
    }

    [Fact]
    public async Task HandleCreateAndApplyJobCommand_WhenJobRequestAlreadyHasAcceptedJob_ShouldReturnFailureResult()
    {
        var command = CreateJobCommandUtils.CreateAndApplyJobCommand();

        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker());

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner());

        _currentUserServiceMock.Setup(
                x =>
                    x.GetUserId())
            .Returns(Guid.NewGuid);

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetDogOwnerJobRequests(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<JobRequest>() {new JobRequest() {Id = command.JobRequestId}});

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                () => new JobRequest()
                {
                    Id = command.JobRequestId,
                    HasAcceptedJob = true
                });

        var handler = new CreateAndApplyJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobRequestErrors.JobRequestAlreadyHasAcceptedJob);
    }


    [Fact]
    public async Task
        HandleCreateAndApplyJobCommand_WhenPaymentFromRequestIsMoreThenPaymentFromJobRequest_ShouldReturnFailureResult()
    {
        var command = CreateJobCommandUtils.CreateAndApplyJobCommand();

        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker());

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner());

        _currentUserServiceMock.Setup(
                x =>
                    x.GetUserId())
            .Returns(Guid.NewGuid);

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetDogOwnerJobRequests(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<JobRequest>() {new JobRequest() {Id = command.JobRequestId}});

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                () => new JobRequest()
                {
                    Id = command.JobRequestId,
                    HasAcceptedJob = false,
                    PaymentTo = Constants.ValidJob.Payment - 1
                });

        var handler = new CreateAndApplyJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.ErrorMessage.Should().Be(JobRequestErrors.JobRequestPaymentIsLessThanRequired);
    }

    [Fact]
    public async Task HandleCreateAndApplyJobCommand_WhenRequestIsValid_ShouldReturnSuccessResult()
    {
        var command = CreateJobCommandUtils.CreateAndApplyJobCommand();

        _walkerRepositoryMock.Setup(
                x =>
                    x.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Walker() {Id = Constants.ValidWalker.Id});

        _dogOwnerRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new DogOwner());

        _currentUserServiceMock.Setup(
                x =>
                    x.GetUserId())
            .Returns(Guid.NewGuid);

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetDogOwnerJobRequests(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<JobRequest>() {new JobRequest() {Id = command.JobRequestId}});

        _jobRequestRepositoryMock.Setup(
                x =>
                    x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                () => new JobRequest()
                {
                    Id = command.JobRequestId,
                    HasAcceptedJob = false,
                    PaymentTo = Constants.ValidJob.Payment + 1
                });

        var handler = new CreateAndApplyJobCommandHandler(
            _jobRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _walkerRepositoryMock.Object,
            _jobRequestRepositoryMock.Object,
            _dogOwnerRepositoryMock.Object);

        var result = await handler.Handle(command, default);

        _jobRepositoryMock.Verify(
            x => x.AddAsync(
                It.Is<Job>(
                    xx =>
                        xx.Payment == command.Payment
                     && xx.DogId == command.DogId
                     && xx.DogOwnerId == command.DogOwnerId
                     && xx.JobRequestId == command.JobRequestId
                     && xx.Comment == command.Comment
                     && xx.Status == JobStatus.Applied
                     && xx.WalkerId == Constants.ValidWalker.Id)),
            Times.Once);
        result.IsSuccess.Should().BeTrue();
        result.ErrorMessage.Should().Be(default);
    }
}