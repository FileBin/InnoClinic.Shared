using Microsoft.AspNetCore.Http;

namespace InnoClinic.Shared.Exceptions.Models;

public class NotFoundException : WebException {
    private static readonly string title = "NotFound";

    public override int StatusCode => StatusCodes.Status404NotFound;

    public static NotFoundException ParameterNotFound(string name) => new($"parameter {name} not found");

    public static NotFoundException NotFoundInDatabase(string name) => new($"{name} not found in database");

    public NotFoundException() : base(title) { }
    public NotFoundException(string? message) : base(title, message) { }
    public NotFoundException(string? message, Exception? innerException) : base(title, message, innerException) { }
}
