namespace Doggo.Application.Requests.Commands.Chat.DeleteUsersFromChatCommand;

using Abstractions.Repositories;
using Abstractions.Services;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Chat;
using Domain.Results;
using MediatR;

public class DeleteUsersFromChatCommandHandler : IRequestHandler<DeleteUsersFromChatCommand, CommonResult>
{
    private readonly ICacheService _cacheService;
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserChatRepository _userChatRepository;

    public DeleteUsersFromChatCommandHandler(
        ICacheService cacheService,
        IChatRepository chatRepository,
        IUserRepository userRepository,
        IUserChatRepository userChatRepository)
    {
        _cacheService = cacheService;
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _userChatRepository = userChatRepository;
    }

    public async Task<CommonResult> Handle(DeleteUsersFromChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetAsync(request.ChatId, cancellationToken);

        if (chat is null)
            return Failure(CommonErrors.EntityDoesNotExist);

        var validUsers = new List<Guid>();

        foreach (var userId in request.UsersId)
        {
            var user = await _userRepository.GetWithPersonalIdentifierAsync(userId, cancellationToken);
            if (user is not null)
                validUsers.Add(userId);
        }

        List<UserChat?> userChatsToDelete = new();

        foreach (var userId in validUsers)
        {
            var userChatToDelete = await _userChatRepository.GetAsync(request.ChatId, userId, cancellationToken);
            if (userChatToDelete is null)
                continue;
            userChatsToDelete.Add(userChatToDelete);
        }

        if (!userChatsToDelete.Any())
            return Failure(CommonErrors.EntitiesDoesNotExist);

        _userChatRepository.RemoveRange(userChatsToDelete!);

        await _cacheService.RemoveDataAsync(CacheKeys.Chat + chat.Id, cancellationToken);

        return Success();
    }
}