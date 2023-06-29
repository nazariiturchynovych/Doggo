namespace Doggo.Api.Application.Requests.Commands.Chat;

using Domain.Constants.ErrorConstants;
using Domain.Entities.Chat;
using Domain.Results;
using Hubs;
using Infrastructure.Repositories.UnitOfWork;
using Infrastructure.Services.CacheService;
using Mappers;
using MediatR;

public record DeleteUsersFromChatCommand(Guid ChatId, ICollection<Guid> UsersId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteUsersFromChatCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public Handler(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<CommonResult> Handle(DeleteUsersFromChatCommand request, CancellationToken cancellationToken)
        {
            var chatRepository = _unitOfWork.GetChatRepository();

            var chat = await chatRepository.GetAsync(request.ChatId, cancellationToken);

            if (chat is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var userRepository = _unitOfWork.GetUserRepository();

            var validUsers = new List<Guid>();

            foreach (var userId in request.UsersId)
            {
                var user = await userRepository.GetAsync(userId, cancellationToken);
                if (user is not null)
                    validUsers.Add(userId);
            }

            var userChatRepository = _unitOfWork.GetUserChatRepository();

            List<UserChat?> userChatsToDelete = new();

            foreach (var userId in validUsers)
            {
                var userChatToDelete = await userChatRepository.GetAsync(request.ChatId, userId, cancellationToken);
                if (userChatToDelete is null)
                    continue;
                userChatsToDelete.Add(userChatToDelete);
            }

            if (!userChatsToDelete.Any())
                return Failure(CommonErrors.EntitiesDoesNotExist);

            userChatRepository.RemoveRange(userChatsToDelete!);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // await DoggoHub.UpdateChat(request.ChatId, chat.MapChatToGetChatDto(), _cacheService);

            return Success();
        }
    }
}