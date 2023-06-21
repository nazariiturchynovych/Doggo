namespace Doggo.Application.Requests.Commands.Walker.PossibleSchedule;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record DeletePossibleScheduleCommand(Guid PossibleScheduleId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeletePossibleScheduleCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(DeletePossibleScheduleCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetPossibleScheduleRepository();

            var possibleSchedule = await repository.GetAsync(request.PossibleScheduleId, cancellationToken);

            if (possibleSchedule is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(possibleSchedule);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}