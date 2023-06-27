namespace Doggo.Api.Application.Requests.Commands.Chat;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record DeleteChatCommand(Guid ChatId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteChatCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetChatRepository();

            var chat = await repository.GetAsync(request.ChatId, cancellationToken);

            if (chat is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(chat);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}