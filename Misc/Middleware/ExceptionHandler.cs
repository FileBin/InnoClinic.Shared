using InnoClinic.Shared.Exceptions.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Shared.Misc.Middleware;

internal sealed class ExceptionHandler : IExceptionHandler {

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        var statusCode = GetStatusCode(exception);

        const string contentType = "application/problem+json";
        
        httpContext.Response.ContentType = contentType;
        httpContext.Response.StatusCode = statusCode;

        ProblemDetails problemDetails = new() {
            Title = GetTitle(exception),
            Detail = exception.Message,
            Type = exception.GetType().Name,
            Status = statusCode,
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);


        return true;
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch {
            WebException we => we.StatusCode,
            System.ComponentModel.DataAnnotations.ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch {
            WebException we => we.Title,
            _ => "Internal Server Error"
        };
}
