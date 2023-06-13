namespace Doggo.Domain.Results;

public record CommonValidationResult : CommonResult
{

    public CommonValidationResult(string errorMessage, string[] errors, Exception? exception = null)
        : base(errorMessage, exception)
    {
        Errors = errors;
    }

    public string[] Errors { get; set; }
}