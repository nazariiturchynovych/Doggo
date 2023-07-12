namespace Doggo.Domain.Results;

using Abstract;

public record CommonResult : ICommonResult
{
    public CommonResult()
    {
        IsSuccess = true;
        ErrorMessage = default!;
        ExceptionMessage = null;
    }

    public CommonResult(string errorMessage, Exception? exception = null)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        ExceptionMessage = exception?.Message;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string ErrorMessage { get; }

    public string? ExceptionMessage { get; }
    
}