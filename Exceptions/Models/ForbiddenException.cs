using Microsoft.AspNetCore.Http;

namespace InnoClinic.Shared.Exceptions.Models;

public class ForbiddenException : WebException {

    public override int StatusCode => StatusCodes.Status403Forbidden;
    private static readonly string title = "Forbidden";

    public ForbiddenException() : base(title) { }
    public ForbiddenException(string? message) : base(title, message) { }
    public ForbiddenException(string? message, Exception? innerException) : base(title, message, innerException) { }
}