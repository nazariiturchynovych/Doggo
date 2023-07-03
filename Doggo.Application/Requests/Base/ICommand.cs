namespace Doggo.Application.Requests.Base;

using MediatR;

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}