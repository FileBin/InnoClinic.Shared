using InnoClinic.Shared.Domain.Abstractions;

namespace InnoClinic.Shared.Messaging.Contracts.Models;

public record ServiceDeleted : IEntity {
    public Guid Id { get; init; }

}
