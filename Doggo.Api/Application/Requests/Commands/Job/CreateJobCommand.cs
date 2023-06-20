namespace Doggo.Application.Requests.Commands.Job;

using Domain.Entities.Job;
using Domain.Enums;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public record CreateJobCommand(
    int WalkerId,
    int DogOwnerId,
    int JobRequestId,
    string Comment) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateJobCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<CommonResult> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetJobRepository();

            await repository.AddAsync(new Job()
            {
                DogOwnerId = request.DogOwnerId,
                JobRequestId = request.JobRequestId,
                CreatedDate = DateTime.UtcNow,
                Comment = request.Comment,
                Status = JobStatus.Pending,
            });

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}