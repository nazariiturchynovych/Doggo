namespace Doggo.Application.Requests.Commands.Walker.PossibleSchedule;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public record DeletePossibleScheduleCommand(Guid PossibleScheduleId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeletePossibleScheduleCommand, CommonResult>
    {
        private readonly IPossibleScheduleRepository _possibleScheduleRepository;


        public Handler(IPossibleScheduleRepository possibleScheduleRepository)
        {
            _possibleScheduleRepository = possibleScheduleRepository;
        }

        public async Task<CommonResult> Handle(DeletePossibleScheduleCommand request, CancellationToken cancellationToken)
        {
            var possibleSchedule = await _possibleScheduleRepository.GetAsync(request.PossibleScheduleId, cancellationToken);

            if (possibleSchedule is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            _possibleScheduleRepository.Remove(possibleSchedule);

            return Success();
        }
    }
}