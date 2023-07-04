namespace Doggo.Application.Requests.Commands.Walker.UpdateWalkerCommand;

using Abstractions.Persistence.Read;
using Abstractions.Repositories;
using Domain.Constants.ErrorConstants;
using Domain.Results;
using Mappers;
using MediatR;

public class UpdateWalkerCommandHandler : IRequestHandler<UpdateWalkerCommand, CommonResult>
{
    private readonly IWalkerRepository _walkerRepository;


    public UpdateWalkerCommandHandler(IWalkerRepository walkerRepository)
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