namespace Shared.Domain.Abstractions;

public interface IRepository<T> {
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetPageAsync(IPageDesc pageDesc, CancellationToken cancellationToken = default);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
