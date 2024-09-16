namespace InnoClinic.Shared.Domain.Abstractions;

public interface IEntity {
    Guid Id { get; }
}

public interface INamedEntity : IEntity {
    string Name { get; }
}