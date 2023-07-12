namespace Doggo.Presentation.Extensions;

using Domain.Results.Abstract;
using Domain.Results.ActionResult;
using Microsoft.AspNetCore.Mvc;

public static class ActionResultExtensions
{
    public static IActionResult ToActionResult(this ICommonResult result)
        => new DoggoActionResult(result);
}