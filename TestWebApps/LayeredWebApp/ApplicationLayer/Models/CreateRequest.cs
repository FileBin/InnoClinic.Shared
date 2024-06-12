namespace InnoClinic.Shared.LayeredWebApp.ApplicationLayer.Models;

public record CreateRequest {
    public required string Name { get; init; }
}
