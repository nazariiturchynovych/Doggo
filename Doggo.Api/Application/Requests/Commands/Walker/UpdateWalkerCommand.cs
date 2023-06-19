namespace Doggo.Application.Requests.Commands.Walker;

using Domain.Constants.ErrorConstants;
using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using Mappers;
using MediatR;

public record UpdateWalkerCommand(int WalkerId ,string? Skills, string? About) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateWalkerCommand, CommonResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommonResult> Handle(UpdateWalkerCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetWalkerRepository();

            var currentWalker = await repository.GetAsync(request.WalkerId, cancellationToken);

            if (currentWalker is null)
                return Failure(CommonErrors.InnerError);

            var updatedWalker = request.MapWalkerUpdateCommandToWalker(currentWalker);

            repository.Update(updatedWalker);

            return Success();
        }
    }
}