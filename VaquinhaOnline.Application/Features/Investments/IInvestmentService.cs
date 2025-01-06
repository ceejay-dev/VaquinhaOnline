namespace VaquinhaOnline.Application.Features.Investments;

public interface IInvestmentService
{
    Task<Result<Guid>> CreateInvestment(InvestmentCreateDto investmentDto, CancellationToken cancellationToken);    
}