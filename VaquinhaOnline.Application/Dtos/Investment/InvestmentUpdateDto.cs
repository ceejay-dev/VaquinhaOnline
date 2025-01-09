namespace VaquinhaOnline.Application.Dtos.Investment;

public record InvestmentUpdateDto(
    Guid Id, double Value,
    string Description, string InvestmentType);
