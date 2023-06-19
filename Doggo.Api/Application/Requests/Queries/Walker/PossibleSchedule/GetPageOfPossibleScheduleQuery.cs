namespace Doggo.Application.Requests.Queries.Walker.PossibleSchedule;

using Domain.DTO;
using Domain.DTO.Walker.PossibleSchedule;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetPageOfPossibleScheduleQuery(int Count, int Page) : IRequest<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
{
    public class Handler : IRequestHandler<GetPageOfPossibleScheduleQuery, CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>> Handle(
            GetPageOfPossibleScheduleQuery request,
            CancellationToken cancellationToken)
        {
            var possibleScheduleRepository = _unitOfWork.GetPossibleScheduleRepository();

            var page = await possibleScheduleRepository.GetPageOfPossibleSchedulesAsync(request.Count, request.Page, cancellationToken);

            return Success(page.MapPossibleScheduleCollectionToPageOPossibleSchedulesDto());
        }
    }
}