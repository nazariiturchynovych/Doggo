namespace Doggo.Application.Requests.Commands.Walker;

using Abstractions.Persistence.Read;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public record DeleteWalkerCommand(Guid WalkerId) : IRequest<CommonResult>
{
    public class Handler : IRequestHandler<DeleteWalkerCommand, CommonResult>
    {
        private readonly IWalkerRepository _walkerRepository;


        public Handler(IWalkerRepository walkerRepository)
        {
            _walkerRepository = walkerRepository;
        }

        public async Task<CommonResult> Handle(DeleteWalkerCommand request, CancellationToken cancellationToken)
        {
            var walker = await _walkerRepository.GetAsync(request.WalkerId, cancellationToken);

            if (walker is null)
                return Failure(CommonErrors.EntityDoesNotExist);

            _walkerRepository.Remove(walker);

            return Success();
        }
    };
};