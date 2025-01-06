using VaquinhaOnline.Domain.Interfaces.IRepository;

namespace VaquinhaOnline.Infrastructure.Repositories;

public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : Entity
{
    public async Task<Result<Guid>> Create(T entity, CancellationToken cancellationToken)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
        return Result.Succeed(entity.Id);
    }

    public async Task<Result> Delete(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
        return Result.Succeed();
    }

    public async Task<Result> Update(T entity, CancellationToken cancellationToken)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
        return Result.Succeed();
    }
}
