namespace Doggo.Application.ResultFactory;

using Domain.Results;
using Domain.Results.Abstract;

public static class ResultFactory
{
    public static CommonResult Success()
        => new();

    public static CommonResult<TData> Success<TData>(TData data)
        => new(data);

    public static CommonResult Failure(string errorMessage, Exception? exception = null)
        => new(errorMessage, exception);

    public static CommonResult<TData> Failure<TData>(
        string errorMessage,
        Exception? exception = null)
        => new(errorMessage, exception);

    public static CommonResult<TData> Failure<TData, TSourceData>(ICommonResult<TSourceData> commonResult)
        => commonResult.IsFailure
            ? Failure<TData>(commonResult.ErrorMessage, commonResult.Exception)
            : throw new ArgumentException("Can not create failure result from success result", nameof(commonResult));
}