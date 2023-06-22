namespace Doggo.Application.Requests.Commands.Walker.PossibleSchedule;

using Domain.Entities.Walker.Schedule;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record CreatePossibleScheduleCommand(Guid WalkerId ,DateTime From, DateTime To) : IRequest<CommonResult>
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

            await repository.AddAsync(
                new PossibleSchedule
                {
                    From = request.From.ToUniversalTime(),
                    To = request.To.ToUniversalTime(),
                    WalkerId = request.WalkerId
                });


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}