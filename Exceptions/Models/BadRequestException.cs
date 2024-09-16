using Microsoft.AspNetCore.Http;

namespace InnoClinic.Shared.Exceptions.Models;

public class BadRequestException : WebException {
    private static readonly string title = "BadRequest";
    public BadRequestException() : base(title) { }
    public BadRequestException(string? message) : base(title, message) { }
    public BadRequestException(string? message, Exception? innerException) : base(title, message, innerException) { }

    public override int StatusCode => StatusCodes.Status400BadRequest;
}