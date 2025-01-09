namespace VaquinhaOnline.Application.Dtos.Investment;

public record InvestmentGetDto (Guid Id, double Value, DateTime InvestmentDate,
    string Description, string InvestmentType, Guid ProjectId, Guid UserId);
