namespace Doggo.Domain.Results;

using Abstract;

public record Result : IResult
{
    public Result()
    {
        IsSuccess = true;
        ErrorCode = default!;
        Exception = null;
    }

    public Result(ErrorCode errorCode, Exception? exception = null)
    {
        IsSuccess = false;
        ErrorCode = errorCode;
        Exception = exception;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public ErrorCode ErrorCode { get; }

    public Exception? Exception { get; }
    
}