using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VaquinhaOnline.Application.Features.Investments;

public class InvestmentService(IInvestmentRepository investmentRepository,
    IValidator<InvestmentCreateDto> validator, IProjectRepository projectRepository) : IInvestmentService
{
    private readonly IInvestmentRepository investmentRepository = investmentRepository;
    private readonly IProjectRepository projectRepository = projectRepository;
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

        var existingProject = await projectRepository.GetProjectById(investmentDto.ProjectId, cancellationToken);

        if (existingProject == null)
        {
            return Result.Failure<Guid>(Error.Invalid("Error.NotFound", "The project was not found."));
        }

        var investment = new Investment(
            value: investmentDto.Value,
            investmentDate: DateTime.UtcNow,
            description: investmentDto.Description,
            investmentType: investmentDto.InvestmentType,
            projectId: investmentDto.ProjectId,
            userId: investmentDto.UserId
        );

        existingProject.UpdateCurrentValue(investmentDto.Value);
        await projectRepository.Update(existingProject, cancellationToken);
        var result = await investmentRepository.Create(investment, cancellationToken);

        return result;
    }

    public async Task<Result> DeleteInvestment(Guid Id, CancellationToken cancellationToken)
    {
        var investment = await investmentRepository.GetInvestmentById(Id, cancellationToken);

        if (investment == null)
        {
            return Result.Failure(Error.NotFound("NotFound", "The investment was not found."));
        }

        var result = await investmentRepository.Delete(investment, cancellationToken);
        return result;
    }

    public async Task<ResultPaginated<List<InvestmentGetDto>>> GetAllInvestments(int PageNumber, int PageSize, CancellationToken cancellationToken)
    {
        var investmentQuery = investmentRepository.GetAllInvestments();

        //Counting 
        var totalCount = investmentQuery.Count();

        if (totalCount == 0)
        {
            return Result.Failure<InvestmentGetDto>(
                error: Error.NotFound("NotFound", "No investment found."),
                totalCount: totalCount,
                currentPage: PageNumber,
                pageSize: PageSize);
        }

        // Calculating the pagination
        var pageNumber = PageNumber < 1 ? 1 : PageNumber;
        var pageSize = PageSize < 1 ? 10 : PageSize;

        var investments = await investmentQuery
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var investmentDtos = investments.Adapt<List<InvestmentGetDto>>();

        return ResultPaginated<List<InvestmentGetDto>>.Success(
            values: investmentDtos,
            totalCount: totalCount,
            currentPage: pageNumber,
            pageSize: pageSize);
    }

    public async Task<Result<InvestmentGetDto>> GetInvestmentById(Guid Id, CancellationToken cancellationToken)
    {
        var existingInvestment = await investmentRepository.GetInvestmentById(Id, cancellationToken);

        if (existingInvestment == null) {
            return Result.Failure<InvestmentGetDto>(Error.NotFound("NotFound", "The investment was not found."));
        }

        var dto = existingInvestment.Adapt<InvestmentGetDto>();

        return Result.Succeed(dto);
    }

    public async Task<Result> UpdateInvestment(InvestmentUpdateDto investment, CancellationToken cancellationToken)
    {
        var dto = investment.Adapt<InvestmentCreateDto>();

          var validationResult = validator.Validate(dto);

        if (!validationResult.IsValid)
        {
            return Result.Failure(
                Error.Invalid("Error.Validation", validationResult.ToString())
            );
        }

        var existingInvestment = await investmentRepository.GetInvestmentById(investment.Id, cancellationToken);

        if (existingInvestment == null)
        {
            return Result.Failure(Error.NotFound("Error.NotFound", "The investment was not found."));
        }

        existingInvestment.Update(
            value: dto.Value,
            description: dto.Description,
            investmentType: dto.InvestmentType
        );

        var result = await investmentRepository.Update(existingInvestment, cancellationToken);
        return result;
    }
}