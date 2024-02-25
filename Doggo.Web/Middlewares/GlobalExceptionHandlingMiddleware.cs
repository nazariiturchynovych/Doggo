namespace Doggo.Api.Middlewares;

using System.Net;
using System.Text.Json;
using Domain.Results;
using Microsoft.AspNetCore.Mvc;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var result = new CommonResult("An internal server error has occured", e);

            // ProblemDetails problemDetails = new ProblemDetails()
            // {
            //     Status = (int) HttpStatusCode.InternalServerError,
            //     Type = "Server Error",
            //     Title = "Server Error",
            //     Detail = "An internal server has occured",
            // }; //TODO delete it
            var json = JsonSerializer.Serialize(result, options);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);

        }
    }
}