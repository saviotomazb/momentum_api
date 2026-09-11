using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Momentum.Application.Exceptions;

namespace Momentum.API.Exceptions;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Unhandled exception occurred while processing the request.");

        var problemDetails = exception switch
        {
            ConflictException conflictException => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Detail = conflictException.Message
            },

            NotFoundException notFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = notFoundException.Message
            },

            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Ocorreu um erro interno no servidor."
            }
        };

        if (exception is ConflictException conflict)
        {
            problemDetails.Extensions["code"] = conflict.Code;
        }
        else if (exception is NotFoundException notFound)
        {
            problemDetails.Extensions["code"] = notFound.Code;
        }
        else
        {
            problemDetails.Extensions["code"] = "INTERNAL_SERVER_ERROR";
        }

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}