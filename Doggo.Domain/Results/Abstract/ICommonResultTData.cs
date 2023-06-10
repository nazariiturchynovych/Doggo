namespace Doggo.Domain.Results.Abstract;

public interface ICommonResult<out TData> : ICommonResult
{
    public TData Data { get; }
}