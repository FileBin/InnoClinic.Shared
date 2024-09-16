using InnoClinic.Shared.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Shared.Misc.Repository;

public abstract class CrudRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity {
    protected abstract DbSet<TEntity> GetDbSet();

    public void Create(TEntity entity) {
        GetDbSet().Add(entity);
    }

    public void Delete(TEntity entity) {
        GetDbSet().Remove(entity);
    }

    public virtual IQueryable<TEntity> GetAll() {
        return GetDbSet().AsNoTracking();
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) {
        return GetAll().SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public void Update(TEntity entity) {
        GetDbSet().Entry(entity).State = EntityState.Modified;
    }
}