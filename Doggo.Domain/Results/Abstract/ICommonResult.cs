namespace Doggo.Domain.Results.Abstract;

public interface ICommonResult
{
    bool IsSuccess { get; }

    bool IsFailure { get; }

    Exception? Exception { get; }

    string ErrorMessage { get; }
}
