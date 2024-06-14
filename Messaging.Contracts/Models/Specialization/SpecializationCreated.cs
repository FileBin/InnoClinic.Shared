namespace InnoClinic.Shared.Messaging.Contracts.Models.Specialization;

public record SpecializationCreated {
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required bool IsActive { get; init; }
}
