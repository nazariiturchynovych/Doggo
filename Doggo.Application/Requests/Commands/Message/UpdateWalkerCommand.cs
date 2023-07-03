namespace Doggo.Application.Requests.Commands.Message;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record UpdateMessageCommand(Guid MessageId ,string Value) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateMessageCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetMessageRepository();

            var currentMessage = await repository.GetAsync(request.MessageId, cancellationToken);

            if (currentMessage is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var updatedMessage = request.MapMessageUpdateCommandToMessage(currentMessage);

            repository.Update(updatedMessage);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    }
}