using VaquinhaOnline.Domain.Entities;

namespace VaquinhaOnline.Domain.Interfaces.IRepository;

public interface IInvestmentRepository : IBaseRepository<Investment>
{
    Task<Investment> GetInvestmentById(Guid id, CancellationToken cancellationToken);
    IQueryable<Investment> GetAllInvestments();
}
