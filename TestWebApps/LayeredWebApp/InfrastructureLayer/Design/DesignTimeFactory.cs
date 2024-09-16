using InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Design;

internal class TestContextFactory : IDesignTimeDbContextFactory<TestDbContext>
{
    public TestDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
        return new TestDbContext(optionsBuilder.Options);
    }
}

