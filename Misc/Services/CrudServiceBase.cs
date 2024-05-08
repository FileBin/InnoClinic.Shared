using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Misc.Repository;
using Mapster;

namespace InnoClinic.Shared.Misc.Services;

public class CrudServiceBase<TEntity, TResponse, TCreateRequest, TUpdateRequest>(IRepository<TEntity> repository, IUnitOfWork unitOfWork) where TEntity: class, IEntity {
    public async Task<TResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        var entity = await repository.GetByIdOrThrow(id, cancellationToken);
        return entity.Adapt<TResponse>();
    }

    public async Task<IEnumerable<TResponse>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default) {
        var entities = await repository.GetPageAsync(pageDesc, cancellationToken);
        return entities.Select(o => o.Adapt<TResponse>()).ToList();
    }

    public async Task<Guid> CreateAsync(TCreateRequest createRequest, CancellationToken cancellationToken = default) {
        var entity = createRequest.Adapt<TEntity>();
        repository.Create(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) {
        await repository.DeleteByIdAsync(id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid id, TUpdateRequest updateRequest, CancellationToken cancellationToken = default) {
        var entity = await repository.GetByIdOrThrow(id, cancellationToken);
        updateRequest.Adapt(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}