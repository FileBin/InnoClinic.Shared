using System.ComponentModel.DataAnnotations;
using InnoClinic.Shared.Exceptions.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Shared.Misc.Middleware;

internal sealed class ExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler {

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {
        httpContext.Response.StatusCode = GetStatusCode(exception);

        return await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext() {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = {
                    Title = GetTitle(exception),
                    Detail = exception.Message,
                    Type = exception.GetType().Name,
                },
            });
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch {
            WebException we => we.StatusCode,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
        
    private static string GetTitle(Exception exception) =>
        exception switch {
            WebException we => we.Title,
            _ => "Internal Server Error"
        };
}
