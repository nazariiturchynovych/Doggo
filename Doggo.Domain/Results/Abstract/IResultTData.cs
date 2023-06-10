namespace Doggo.Domain.Results.Abstract;

public interface IResult<out TData> : IResult
{
    public TData Data { get; }
}