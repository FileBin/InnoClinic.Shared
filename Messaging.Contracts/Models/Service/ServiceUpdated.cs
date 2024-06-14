namespace InnoClinic.Shared.Messaging.Contracts.Models;

public record ServiceUpdated {
    public Guid Id { get; init; }
    public Guid? SpecializationId { get; init; }
    public Guid? CategoryId { get; init; }
    public required string? Name { get; init; }
    public required decimal? Price { get; init; }
    public required bool? IsActive { get; init; }
}
