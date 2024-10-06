using Backend.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Middleware;

public class ExceptionMiddleware
{

    private readonly RequestDelegate Next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.Next(context);
        }
        catch (Exception ex)
        {
            (string Detail, string title, int StatusCode) details = ex switch
            {
                NotFoundException => (
                    ex.Message,
                    ex.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                ValidationException => (
                    ex.Message,
                    ex.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestException => (
                    ex.Message,
                    ex.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                CustomMessageException custom => (
                    custom.Message,
                    ex.GetType().Name,
                    context.Response.StatusCode = custom.StatusCode
                ),
                _ => (
                    ex.Message,
                    ex.GetType().Name,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };

            ProblemDetails problemDetails = new()
            {
                Title = details.title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.Request.Path,
                Type = context.Request.Method
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if (ex is ValidationException validations)
                problemDetails.Extensions.Add("ValidationErrors", validations.Errors);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
