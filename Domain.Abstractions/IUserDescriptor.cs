namespace InnoClinic.Shared.Domain.Abstractions;

public interface IUserDescriptor {
    bool IsAuthenticated();
    bool IsAdmin();
    string? Id { get; }
    string? Name { get; }
}