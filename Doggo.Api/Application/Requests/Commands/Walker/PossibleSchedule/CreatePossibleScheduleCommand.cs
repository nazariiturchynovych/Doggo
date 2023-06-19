namespace Doggo.Application.Requests.Commands.Walker.PossibleSchedule;

using Domain.Constants.ErrorConstants;
using Domain.Entities.Walker.Schedule;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record CreatePossibleScheduleCommand(int WalkerId ,TimeOnly From, TimeOnly To, DayOfWeek DayOfWeek) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreatePossibleScheduleCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(CreatePossibleScheduleCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetPossibleScheduleRepository();

            var possibleSchedule = await repository.GetAsync(request.WalkerId, cancellationToken);

            if (possibleSchedule is not null)
                return Failure(CommonErrors.EntityAlreadyExist);

            await repository.AddAsync(
                new PossibleSchedule
                {
                    From = request.From,
                    To = request.To,
                    DayOfWeek = request.DayOfWeek,
                    WalkerId = request.WalkerId
                });


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}