namespace Doggo.Application.Requests.Commands.Chat;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Services.CacheService;
using MediatR;

public record DeleteChatCommand(Guid ChatId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteChatCommand, CommonResult>
    {
        private readonly ICacheService _cacheService;
        private readonly IChatRepository _chatRepository;

        public Handler(ICacheService cacheService, IChatRepository chatRepository)
        {
            _cacheService = cacheService;
            _chatRepository = chatRepository;
        }

        public async Task<CommonResult> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.GetAsync(request.ChatId, cancellationToken);

            if (chat is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            _chatRepository.Remove(chat);

            await _cacheService.RemoveDataAsync(CacheKeys.Chat + chat.Id, cancellationToken);

            return Success();
        }
    }
}