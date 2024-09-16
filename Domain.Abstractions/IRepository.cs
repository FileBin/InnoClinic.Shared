namespace InnoClinic.Shared.Domain.Abstractions;

public interface IRepository<T> {
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<T> GetAll();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
