namespace Doggo.Domain.Results;

using Abstract;

public record CommonResult : ICommonResult
{
    public CommonResult()
    {
        IsSuccess = true;
        ErrorMessage = default!;
        Exception = null;
    }

    public CommonResult(string errorMessage, Exception? exception = null)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string ErrorMessage { get; }

    public Exception? Exception { get; }
    
}