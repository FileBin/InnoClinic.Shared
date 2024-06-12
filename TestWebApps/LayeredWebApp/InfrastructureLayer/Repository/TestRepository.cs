using Microsoft.EntityFrameworkCore;
using InnoClinic.Shared.Misc.Repository;
using InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Database;
using InnoClinic.Shared.LayeredWebApp.DomainLayer;

namespace InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Repository;

internal class TestRepository(TestDbContext dbContext) : CrudRepositoryBase<TestEntity> {
    protected override DbSet<TestEntity> GetDbSet() => dbContext.Entities;
}