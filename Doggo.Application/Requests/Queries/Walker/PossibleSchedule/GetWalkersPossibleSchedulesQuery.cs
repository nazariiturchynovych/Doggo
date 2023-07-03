namespace Doggo.Application.Requests.Queries.Walker.PossibleSchedule;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using DTO;
using DTO.Walker.PossibleSchedule;
using Mappers;
using MediatR;

public record GetWalkersPossibleSchedulesQuery(Guid WalkerId) : IRequest<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
{
    public class Handler : IRequestHandler<GetWalkersPossibleSchedulesQuery, CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
    {
        private readonly IPossibleScheduleRepository _possibleScheduleRepository;

        public Handler(IPossibleScheduleRepository possibleScheduleRepository)
        {
            _possibleScheduleRepository = possibleScheduleRepository;
        }

        public async Task<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>> Handle(
            GetWalkersPossibleSchedulesQuery request,
            CancellationToken cancellationToken)
        {
            var possibleSchedules
                = await _possibleScheduleRepository.GetWalkerPossibleSchedulesAsync(request.WalkerId, cancellationToken);

            if (!possibleSchedules.Any())
                return Failure<PageOfTDataDto<GetPossibleScheduleDto>>(CommonErrors.EntityDoesNotExist);

            return Success(possibleSchedules.MapPossibleScheduleCollectionToPageOPossibleSchedulesDto());
        }
    }
};