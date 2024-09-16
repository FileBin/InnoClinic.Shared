using InnoClinic.Shared.Domain.Abstractions;

namespace InnoClinic.Shared.Messaging.Contracts.Models.Specialization;

public record SpecializationCreated : INamedEntity {
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required bool IsActive { get; init; }
}
