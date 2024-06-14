namespace InnoClinic.Shared.Messaging.Contracts.Models.Specialization;

public record SpecializationUpdated {
    public Guid Id { get; init; }
    public required string? Name { get; init; }
    public required bool? IsActive { get; init; }
}