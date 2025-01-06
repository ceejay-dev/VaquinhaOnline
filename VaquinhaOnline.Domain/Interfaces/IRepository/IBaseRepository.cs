using VaquinhaOnline.Domain.Results;

namespace VaquinhaOnline.Domain.Interfaces.IRepository;

public interface IBaseRepository<T>
{
    Task<Result<Guid>> Create(T entity, CancellationToken cancellationToken);
    Task<Result> Update(T entity, CancellationToken cancellationToken);
    Task<Result> Delete(T entity, CancellationToken cancellationToken);
}