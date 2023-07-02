namespace Doggo.Application.Requests.Queries.Chat;

using Api.Application.Mappers;
using Domain.Constants.ErrorConstants;
using Domain.DTO;
using Domain.DTO.Chat;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetUserChatsQuery(Guid ChatOwnerId) : IRequest<CommonResult<PageOfTDataDto<ChatDto>>>
{
    public class Handler : IRequestHandler<GetUserChatsQuery, CommonResult<PageOfTDataDto<ChatDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<ChatDto>>> Handle(
            GetUserChatsQuery request,
            CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetChatRepository();

            var chats = await repository.GetUserChatsAsync(request.ChatOwnerId, cancellationToken);

            if (chats is null || !chats.Any())
                return Failure<PageOfTDataDto<ChatDto>>(CommonErrors.EntityDoesNotExist);

            return Success(chats.MapChatCollectionToPageOfChatDto());
        }
    }
};