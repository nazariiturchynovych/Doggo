namespace Doggo.Presentation.Filters;

using Domain.Results.ActionResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ActionResultFilter : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Cancel)
            return;

        if (context.Result is DoggoActionResult result)
        {
            if (result.Result.IsFailure)
            {
                context.Result = new BadRequestObjectResult(result.Result);
                return;
            }

            context.Result = new OkObjectResult(result.Result); //TODO maybe delete this because is easier to manipulate with data and not with exceptions on client
        }
    }
}