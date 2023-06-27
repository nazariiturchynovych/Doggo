namespace Doggo.Api.Application.Requests.Commands.Chat;

using Doggo.Application.Mappers;
using Domain.Constants.ErrorConstants;
using Doggo.Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record UpdateChatCommand(Guid ChatId ,string? Name) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateChatCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetChatRepository();

            var chat = await repository.GetByIdAsync(request.ChatId, cancellationToken);

            if (chat is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var updatedChat = request.MapUpdateChatCommandToChat(chat);

            repository.Update(updatedChat);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}