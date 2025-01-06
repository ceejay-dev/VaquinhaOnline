namespace VaquinhaOnline.Application.Dtos.Project;

public record ProjectCreateDto (string Title, string Description, string Sector,
    string Status, double GoalValue, double CurrentValue, DateTime PublicationDate,
    DateTime ClosingDate, string Progress, Guid UserId);
