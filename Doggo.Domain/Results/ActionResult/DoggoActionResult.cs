namespace Doggo.Domain.Results.ActionResult;

using Abstract;
using Microsoft.AspNetCore.Mvc;

public class DoggoActionResult : ActionResult
{
    public DoggoActionResult(ICommonResult result)
    {
        Result = result;
    }

    public ICommonResult Result { get; }
}