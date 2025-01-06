using VaquinhaOnline.Domain.Interfaces.IRepository;

namespace VaquinhaOnline.Infrastructure.Repositories;

public class InvestmentRepository(ApplicationDbContext context) : BaseRepository<Investment>(context), IInvestmentRepository
{
    public IQueryable<Investment> GetAllInvestments()
    {
        return context.Investments.AsQueryable();
    }

    public async Task<Investment> GetInvestmentById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Investments.FirstOrDefaultAsync(x => x.Id == id);
    }
}
