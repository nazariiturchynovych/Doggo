namespace Doggo.Application.Requests.Commands.PossibleSchedule.DeletePossibleScheduleCommand;

using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public class DeletePossibleScheduleCommandHandler : IRequestHandler<DeletePossibleScheduleCommand, CommonResult>
{
    private readonly IPossibleScheduleRepository _possibleScheduleRepository;


    public DeletePossibleScheduleCommandHandler(IPossibleScheduleRepository possibleScheduleRepository)
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