namespace InnoClinic.Shared.Exceptions.Models;

public abstract class WebException : ApplicationException {
    public abstract int StatusCode { get; }
    public string Title { get; init; }
    protected WebException(string title) => Title = title;
    protected WebException(string title, string? message) : base(message) => Title = title;
    protected WebException(string title, string? message, Exception? innerException)
        : base(message, innerException) => Title = title;
}