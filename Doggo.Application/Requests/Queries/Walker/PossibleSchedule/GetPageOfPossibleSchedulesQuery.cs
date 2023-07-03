namespace Doggo.Application.Requests.Queries.Walker.PossibleSchedule;

using Abstractions.Persistence.Read;
using Domain.Results;
using DTO;
using DTO.Walker.PossibleSchedule;
using Mappers;
using MediatR;

public record GetPageOfPossibleSchedulesQuery(int PageCount, int Page)
    : IRequest<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
{
    public class Handler : IRequestHandler<GetPageOfPossibleSchedulesQuery, CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
    {
        private readonly IPossibleScheduleRepository _possibleScheduleRepository;

        public Handler(IPossibleScheduleRepository possibleScheduleRepository)
        {
            _possibleScheduleRepository = possibleScheduleRepository;
        }

        public async Task<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>> Handle(
            GetPageOfPossibleSchedulesQuery request,
            CancellationToken cancellationToken)
        {
            var page = await _possibleScheduleRepository.GetPageOfPossibleSchedulesAsync(
                request.PageCount,
                request.Page,
                cancellationToken);

            return Success(page.MapPossibleScheduleCollectionToPageOPossibleSchedulesDto());
        }
    }
}