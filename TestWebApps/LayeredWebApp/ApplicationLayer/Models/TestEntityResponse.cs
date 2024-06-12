namespace InnoClinic.Shared.LayeredWebApp.ApplicationLayer.Models;

public record TestEntityResponse {
    public Guid Id { get; init; }

    public required string Name { get; init; }
}