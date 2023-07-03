namespace Doggo.Application.Requests.Commands.Walker.PossibleSchedule;

using Abstractions.Persistence.Read;
using Domain.Entities.Walker.Schedule;
using Domain.Results;
using MediatR;

public record CreatePossibleScheduleCommand(Guid WalkerId, DateTime From, DateTime To) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreatePossibleScheduleCommand, CommonResult>
    {
        private readonly IPossibleScheduleRepository _possibleScheduleRepository;

        public Handler(IPossibleScheduleRepository possibleScheduleRepository)
        {
            _possibleScheduleRepository = possibleScheduleRepository;
        }

        public async Task<CommonResult> Handle(CreatePossibleScheduleCommand request, CancellationToken cancellationToken)
        {
            await _possibleScheduleRepository.AddAsync(
                new PossibleSchedule
                {
                    From = request.From.ToUniversalTime(),
                    To = request.To.ToUniversalTime(),
                    WalkerId = request.WalkerId
                });

            return Success();
        }
    }
}