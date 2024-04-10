namespace InnoClinic.Shared.Domain.Abstractions;

interface IUserDescriptor {
    bool IsAdmin();
    string Id { get; }
    string Name { get; }
}