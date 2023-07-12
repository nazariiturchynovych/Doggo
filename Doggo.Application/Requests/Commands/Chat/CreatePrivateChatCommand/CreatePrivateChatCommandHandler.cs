namespace Doggo.Application.Requests.Commands.Chat.CreatePrivateChatCommand;

using Abstractions.Repositories;
using Domain.Entities.Chat;
using Domain.Results;
using MediatR;

public class CreatePrivateChatCommandHandler : IRequestHandler<CreatePrivateChatCommand, CommonResult>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserChatRepository _userChatRepository;

    public CreatePrivateChatCommandHandler(IChatRepository chatRepository, IUserChatRepository userChatRepository)
    {
        _chatRepository = chatRepository;
        _userChatRepository = userChatRepository;
    }

    public async Task<CommonResult> Handle(CreatePrivateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = new Chat
        {
            Name = request.Name,
            IsPrivate = true
        };

        await _chatRepository.AddAsync(chat);

        var userChats = new List<UserChat>
        {
            new()
            {
                ChatId = chat.Id,
                UserId = request.FirstUserId
            },
            new()
            {
                ChatId = chat.Id,
                UserId = request.SecondUserId
            }
        };

        await _userChatRepository.AddRangeAsync(userChats);

        return Success();
    }
}