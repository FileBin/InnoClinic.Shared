using InnoClinic.Shared.Domain.Abstractions;

namespace InnoClinic.Shared.LayeredWebApp.DomainLayer;

public class TestEntity : IEntity {
    public Guid Id { get; set; }
    public required string Name { get; set; }
}