using InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Database;
using InnoClinic.Shared.Misc;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Shared.LayeredWebApp.InfrastructureLayer.Repository;

internal class UnitOfWork(TestDbContext dbContext) : UnitOfWorkBase {
    public override DbContext GetDbContext() => dbContext;
}
