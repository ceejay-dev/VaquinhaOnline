namespace VaquinhaOnline.Application.Features.Investments;

public interface IInvestmentService
{
    Task<Result<Guid>> CreateInvestment(InvestmentCreateDto investmentDto, CancellationToken cancellationToken);   
    Task<Result> DeleteInvestment (Guid Id, CancellationToken cancellationToken);
    Task<Result> UpdateInvestment(InvestmentUpdateDto investment, CancellationToken cancellationToken);
    Task<Result<InvestmentGetDto>> GetInvestmentById (Guid Id, CancellationToken cancellationToken);
    Task<ResultPaginated<List<InvestmentGetDto>>> GetAllInvestments(int PageNumber, int PageSize, CancellationToken cancellationToken);
}