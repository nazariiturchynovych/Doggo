namespace Doggo.Application.Requests.Commands.Chat.AddUsersToChatCommand;

using Abstractions.Persistence.Read;
using Domain.Constants;
using Domain.Constants.ErrorConstants;
using Domain.Entities.Chat;
using Domain.Results;
using Infrastructure.Services.CacheService;
using MediatR;

public class AddUsersToChatCommandHandler : IRequestHandler<AddUsersToChatCommand, CommonResult>
{
    private readonly ICacheService _cacheService;
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserChatRepository _userChatRepository;

    public AddUsersToChatCommandHandler(ICacheService cacheService, IChatRepository chatRepository, IUserRepository userRepository, IUserChatRepository userChatRepository)
    {
        _cacheService = cacheService;
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _userChatRepository = userChatRepository;
    }

    public async Task<CommonResult> Handle(AddUsersToChatCommand request, CancellationToken cancellationToken)
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

        var userChats = validUsers.Select(
            userId => new UserChat
            {
                ChatId = request.ChatId,
                UserId = userId
            });


        await _userChatRepository.AddRangeAsync(userChats);

        await _cacheService.RemoveDataAsync(CacheKeys.Chat + chat.Id, cancellationToken);
        await _cacheService.SetData(CacheKeys.Chat + chat.Id, chat, cancellationToken);

        return Success();
    }
}