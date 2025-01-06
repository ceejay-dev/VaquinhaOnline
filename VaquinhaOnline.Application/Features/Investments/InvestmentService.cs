namespace VaquinhaOnline.Application.Features.Investments;

public class InvestmentService(IInvestmentRepository investmentRepository, IValidator<InvestmentCreateDto> validator) : IInvestmentService
{
    private readonly IInvestmentRepository investmentRepository = investmentRepository;
    private readonly IValidator<InvestmentCreateDto> validator = validator;

    public async Task<Result<Guid>> CreateInvestment(InvestmentCreateDto investmentDto, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(investmentDto);

        if (!validationResult.IsValid)
        {
            return Result.Failure<Guid>(
                Error.Invalid("Error.Validation", validationResult.ToString())
            );
        }

        var investment = new Investment(
            value: investmentDto.Value,
            investmentDate: investmentDto.InvestmentDate,
            description: investmentDto.Description,
            investmentType: investmentDto.InvestmentType,
            projectId: investmentDto.ProjectId,
            userId: investmentDto.UserId
        );

        var result = await investmentRepository.Create(investment, cancellationToken);

        return result;
    }
}