namespace Doggo.Application.Requests.Base;

using MediatR;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    
}