namespace Doggo.Application.Behaviours;

using Abstractions.Repositories.UnitOfWork;
using Domain.Results;
using MediatR;
using Requests.Base;

public class UnitOfWorkPipeLineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : CommonResult
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkPipeLineBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)

    {
        var response = await next();

        if (response.IsSuccess)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}