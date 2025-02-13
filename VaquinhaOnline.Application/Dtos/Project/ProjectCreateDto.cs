namespace VaquinhaOnline.Application.Dtos.Project;

public record ProjectCreateDto (string Title, string Description, string Sector,
    string Status, double GoalValue, DateTime ClosingDate, Guid UserId);