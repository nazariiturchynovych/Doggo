namespace Doggo.Application.Requests.Commands.Walker;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public record UpdateWalkerCommand(Guid WalkerId, string? Skills, string? About) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<UpdateWalkerCommand, CommonResult>
    {
        private readonly IWalkerRepository _walkerRepository;


        public Handler(IWalkerRepository walkerRepository)
        {
            _walkerRepository = walkerRepository;
        }

        public async Task<CommonResult> Handle(UpdateWalkerCommand request, CancellationToken cancellationToken)
        {
            var currentWalker = await _walkerRepository.GetAsync(request.WalkerId, cancellationToken);

            if (currentWalker is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            var updatedWalker = request.MapWalkerUpdateCommandToWalker(currentWalker);

            _walkerRepository.Update(updatedWalker);

            return Success();
        }
    }
}