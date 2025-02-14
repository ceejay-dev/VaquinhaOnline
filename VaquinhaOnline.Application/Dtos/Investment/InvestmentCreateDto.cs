namespace VaquinhaOnline.Application.Dtos.Investment;

public record InvestmentCreateDto (double Value, 
    string Description, string InvestmentType, Guid ProjectId, Guid UserId);
