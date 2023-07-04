namespace Doggo.Application.Requests.Commands.Walker.DeleteWalkerCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using MediatR;

public class DeleteWalkerCommandHandler : IRequestHandler<DeleteWalkerCommand, CommonResult>
{
    private readonly IWalkerRepository _walkerRepository;


    public DeleteWalkerCommandHandler(IWalkerRepository walkerRepository)
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