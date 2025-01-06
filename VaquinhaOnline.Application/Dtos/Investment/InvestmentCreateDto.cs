namespace VaquinhaOnline.Application.Dtos.Investment;

public record InvestmentCreateDto (double Value, DateTime InvestmentDate, 
    string Description, string InvestmentType, Guid ProjectId, Guid UserId);
