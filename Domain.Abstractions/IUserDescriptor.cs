namespace InnoClinic.Shared.Domain.Abstractions;

public interface IUserDescriptor {
    bool IsAdmin();
    string Id { get; }
    string Name { get; }
}