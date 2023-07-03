namespace Doggo.Application.Requests.Commands.Chat;

using Abstractions.Persistence.Read;
using Domain.Entities.Chat;
using Domain.Results;
using Infrastructure.Services.CurrentUserService;
using MediatR;

public record CreateGroupChatCommand(
    string Name,
    List<Guid> UserIds) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<CreateGroupChatCommand, CommonResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserChatRepository _userChatRepository;

        public Handler(
            ICurrentUserService currentUserService,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IUserChatRepository userChatRepository)
        {
            _currentUserService = currentUserService;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _userChatRepository = userChatRepository;
        }

        public async Task<CommonResult> Handle(CreateGroupChatCommand request, CancellationToken cancellationToken)
        {
            var chat = new Chat
            {
                Name = request.Name,
            };

            await _chatRepository.AddAsync(chat);

            var validUsers = new List<Guid>();

            foreach (var userId in request.UserIds)
            {
                var user = await _userRepository.GetWithPersonalIdentifierAsync(userId, cancellationToken);
                if (user is not null)
                    validUsers.Add(userId);
            }

            var userChats = validUsers.Select(
                    userId => new UserChat
                    {
                        ChatId = chat.Id,
                        UserId = userId
                    })
                .ToList();

            userChats.Add(
                new UserChat
                {
                    ChatId = chat.Id,
                    UserId = _currentUserService.GetUserId()
                });

            await _userChatRepository.AddRangeAsync(userChats);

            return Success();
        }
    }
}