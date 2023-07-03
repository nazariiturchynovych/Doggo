namespace Doggo.Application.Behaviours;

using Domain.Results;
using Infrastructure.Repositories.UnitOfWork;
using MediatR;
using Requests.Base;

public class SavingChangesPipeLineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : CommonResult
{
    private readonly IUnitOfWork _unitOfWork;

    public SavingChangesPipeLineBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)

    {

        var response = await next();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}