namespace Doggo.Application.Requests.Queries.Walker.PossibleSchedule;

using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.DTO.Walker.PossibleSchedule;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetWalkersPossibleSchedulesQuery(Guid WalkerId) : IRequest<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
{
    public class Handler : IRequestHandler<GetWalkersPossibleSchedulesQuery, CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetPossibleScheduleDto>>> Handle(
            GetWalkersPossibleSchedulesQuery request,
            CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetPossibleScheduleRepository();

            var possibleSchedules = await repository.GetWalkerPossibleSchedulesAsync(request.WalkerId, cancellationToken);

            if (!possibleSchedules.Any())
                return Failure<PageOfTDataDto<GetPossibleScheduleDto>>(CommonErrors.EntityDoesNotExist);

            return Success(possibleSchedules.MapPossibleScheduleCollectionToPageOPossibleSchedulesDto());
        }
    }
};