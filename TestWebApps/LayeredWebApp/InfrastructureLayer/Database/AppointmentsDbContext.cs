using InnoClinic.Shared.LayeredWebApp.DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Database;

internal class TestDbContext(DbContextOptions options) : DbContext(options) {
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite("Data Source=testdb");
    }
    public DbSet<TestEntity> Entities => Set<TestEntity>();
}
