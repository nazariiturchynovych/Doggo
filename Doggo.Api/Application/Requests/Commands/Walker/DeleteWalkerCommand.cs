namespace Doggo.Application.Requests.Commands.Walker;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;

public record DeleteWalkerCommand(Guid WalkerId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteWalkerCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(DeleteWalkerCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetWalkerRepository();

            var walker = await repository.GetAsync(request.WalkerId, cancellationToken);

            if (walker is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            repository.Remove(walker);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success();
        }
    };
};