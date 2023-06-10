namespace Doggo.Domain.Results;

using Abstract;

public record Result<TData> : Result, IResult<TData>
{
    public Result(TData data)
    {
        Data = data;
    }

    public Result(
        ErrorCode errorCode,
        Exception? exception = null)
        : base(errorCode, exception)
    {
        Data = default!;
    }

    public TData Data { get; }

}