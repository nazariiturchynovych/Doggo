namespace Doggo.Domain.Results.Abstract;

public interface IResult
{
    bool IsSuccess { get; }

    bool IsFailure { get; }

    Exception? Exception { get; }

    ErrorCode ErrorCode { get; }
}
