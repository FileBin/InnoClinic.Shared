using InnoClinic.Shared.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace InnoClinic.Shared.Misc;

public abstract class UnitOfWorkBase : IUnitOfWork {
    public abstract DbContext GetDbContext();

    public void RevertChanges() {
        var changedEntries = GetDbContext().ChangeTracker.Entries()
            .Where(x => x.State != EntityState.Unchanged).ToList();

        foreach (var entry in changedEntries) {
            switch (entry.State) {
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        return GetDbContext().SaveChangesAsync(cancellationToken);
    }
}
