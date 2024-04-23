using Microsoft.EntityFrameworkCore.Storage;

namespace InnoClinic.Shared.Domain.Abstractions;

public interface IUnitOfWork {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void RevertChanges();

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

}

