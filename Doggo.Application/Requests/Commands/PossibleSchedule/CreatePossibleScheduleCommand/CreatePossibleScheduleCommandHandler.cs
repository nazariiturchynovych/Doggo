namespace Doggo.Application.Requests.Commands.PossibleSchedule.CreatePossibleScheduleCommand;

using Abstractions.Persistence.Read;
using Domain.Entities.Walker.Schedule;
using Domain.Results;
using MediatR;

public class CreatePossibleScheduleCommandHandler : IRequestHandler<CreatePossibleScheduleCommand, CommonResult>
{
    private readonly IPossibleScheduleRepository _possibleScheduleRepository;

    public CreatePossibleScheduleCommandHandler(IPossibleScheduleRepository possibleScheduleRepository)
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