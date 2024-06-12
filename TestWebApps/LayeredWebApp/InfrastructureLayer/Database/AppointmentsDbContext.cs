using InnoClinic.Shared.LayeredWebApp.DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Database;

internal class TestDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<TestEntity> Entities => Set<TestEntity>();
}
