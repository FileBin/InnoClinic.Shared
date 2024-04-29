using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Shared.Misc.Repository;

public static class RepositoryExtensions {
    public static Task<bool> ExistsAsync<T>(this IRepository<T> repository, Guid id, CancellationToken cancellationToken = default) where T : IEntity {
        return repository.GetAll().AnyAsync(entity => entity.Id == id, cancellationToken);
    }

    public static async Task EnsureExistsAsync<T>(this IRepository<T> repository, Guid id, CancellationToken cancellationToken = default) where T : IEntity {
        var exists = await repository.ExistsAsync(id, cancellationToken);

        if (!exists) {
            throw new NotFoundException($"{typeof(T).Name} with id {id} not found");
        }
    }
}