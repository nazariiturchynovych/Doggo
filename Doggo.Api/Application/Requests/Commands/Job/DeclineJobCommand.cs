namespace Doggo.Application.Requests.Commands.Job;

using Domain.Constants.ErrorConstants;
using Domain.Enums;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public record DeclineJobCommand(
    Guid JobId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeclineJobCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<CommonResult> Handle(DeclineJobCommand request, CancellationToken cancellationToken)
        {
            var jobRepository = _unitOfWork.GetJobRepository();

            var job = await jobRepository.GetAsync(request.JobId, cancellationToken);

            if (job is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var dogOwnerRepository = _unitOfWork.GetDogOwnerRepository();

            var currentDogOwner = await dogOwnerRepository.GetByUserIdAsync(_currentUserService.GetUserId(), cancellationToken);

            if (currentDogOwner!.Jobs.All(x => x.Id != job.Id))
                return Failure(JobErrors.CurrenDogOwnerIsNotOwnerOfThisJob);

            job.Status = JobStatus.Declined;

            jobRepository.Update(job);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}