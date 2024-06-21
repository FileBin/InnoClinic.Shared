using InnoClinic.Shared.Domain.Abstractions;

namespace InnoClinic.Shared.Messaging.Contracts.Models.Specialization;

public record SpecializationDeleted : IEntity {
    public Guid Id { get; init; }
}
