namespace Doggo.Api.Application.Requests.Commands.Chat;

using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using MediatR;

public record DeleteChatCommand(Guid ChatId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteChatCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetChatRepository();

            var chat = await repository.GetAsync(request.ChatId, cancellationToken);

            if (chat is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(chat);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _cacheService.RemoveDataAsync(CacheKeys.Chat + chat.Id, cancellationToken);

            return Success();
        }
    }
}