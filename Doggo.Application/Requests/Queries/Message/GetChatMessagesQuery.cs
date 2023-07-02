namespace Doggo.Api.Application.Requests.Queries.Message;

using Domain.DTO;
using Domain.DTO.Chat.Message;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record GetChatMessagesQuery(
    Guid ChatId,
    int Count) : IRequest<CommonResult<PageOfTDataDto<GetMessageDto>>>
{
    public class Handler : IRequestHandler<GetChatMessagesQuery, CommonResult<PageOfTDataDto<GetMessageDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult<PageOfTDataDto<GetMessageDto>>> Handle(
            GetChatMessagesQuery request,
            CancellationToken cancellationToken)
        {
            var dogRepository = _unitOfWork.GetMessageRepository();

            var page = await dogRepository.GetChatMessagesAsync(
                request.ChatId,
                request.Count,
                cancellationToken);

            return Success(page.MapMessageCollectionToPageOfMessageDto());
        }
    }
}