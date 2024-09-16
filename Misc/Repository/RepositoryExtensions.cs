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
            throw new NotFoundException($"{typeof(T).Name} with id {id} was not found");
        }
    }

    public static async Task<T> GetByIdOrThrow<T>(this IRepository<T> repository, Guid id, CancellationToken cancellationToken = default) where T : IEntity {
        return await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"{typeof(T).Name} with id {id} was not found");
    }

    public static async Task DeleteByIdAsync<T>(this IRepository<T> repository, Guid id, CancellationToken cancellationToken = default) where T : IEntity {
        T entity = await repository.GetByIdOrThrow(id, cancellationToken);
        repository.Delete(entity);
    }
}