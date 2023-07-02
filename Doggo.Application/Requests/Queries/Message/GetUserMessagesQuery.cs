namespace Doggo.Api.Application.Requests.Queries.Message;

using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.DTO.Chat.Message;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetUserMessagesQuery(Guid UserId) : IRequest<CommonResult<PageOfTDataDto<GetMessageDto>>>
{
    public class Handler : IRequestHandler<GetUserMessagesQuery, CommonResult<PageOfTDataDto<GetMessageDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetMessageDto>>> Handle(
            GetUserMessagesQuery request,
            CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetMessageRepository();

            var dogs = await repository.GetUserMessagesAsync(request.UserId, cancellationToken);

            if (dogs is null || !dogs.Any())
                return Failure<PageOfTDataDto<GetMessageDto>>(CommonErrors.EntityDoesNotExist);

            return Success(dogs.MapMessageCollectionToPageOfMessageDto());
        }
    }
};