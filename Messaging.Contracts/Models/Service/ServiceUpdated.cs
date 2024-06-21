using InnoClinic.Shared.Domain.Abstractions;

namespace InnoClinic.Shared.Messaging.Contracts.Models;

public record ServiceUpdated : IEntity {
    public Guid Id { get; init; }
    public Guid? SpecializationId { get; init; }
    public Guid? CategoryId { get; init; }
    public required string? Name { get; init; }
    public required decimal? Price { get; init; }
    public required bool? IsActive { get; init; }
}
