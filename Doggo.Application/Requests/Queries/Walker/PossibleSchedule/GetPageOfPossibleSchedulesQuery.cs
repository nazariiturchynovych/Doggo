namespace Doggo.Application.Requests.Queries.Walker.PossibleSchedule;

using Domain.Results;
using DTO;
using DTO.Walker.PossibleSchedule;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfPossibleSchedulesQuery(int PageCount, int Page) : IRequest<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
{
    public class Handler : IRequestHandler<GetPageOfPossibleSchedulesQuery, CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>> Handle(
            GetPageOfPossibleSchedulesQuery request,
            CancellationToken cancellationToken)
        {
            var possibleScheduleRepository = _unitOfWork.GetPossibleScheduleRepository();

            var page = await possibleScheduleRepository.GetPageOfPossibleSchedulesAsync(request.PageCount, request.Page, cancellationToken);

            return Success(page.MapPossibleScheduleCollectionToPageOPossibleSchedulesDto());
        }
    }
}