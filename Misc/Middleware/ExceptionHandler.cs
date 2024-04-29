using InnoClinic.Shared.Exceptions.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Shared.Misc.Middleware;

internal sealed class ExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler {

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        var statusCode = GetStatusCode(exception);
        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext() {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = {
                    Title = GetTitle(exception),
                    Detail = exception.Message,
                    Type = exception.GetType().Name,
                    Status = statusCode,
                },
            });
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
