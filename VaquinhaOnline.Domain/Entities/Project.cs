using VaquinhaOnline.Domain.DomainObjects;

namespace VaquinhaOnline.Domain.Entities;

public class Project : Entity
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Sector { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public double GoalValue { get; private set; }
    public double CurrentValue { get; private set; }
    public DateTime PublicationDate { get; private set; }
    public DateTime ClosingDate { get; private set; }
    public string Progress { get; private set; } = string.Empty;


    //navegation properties
    public Guid UserId { get; set; }
    public Guid InvestmentId { get; set; }
    public virtual Investment Investment { get; set; }
    public Project()
    {
    }

    public Project(string title, string description, string sector, string status, double goalValue, double currentValue, DateTime publicationDate, DateTime closingDate, string progress, Guid userId)
    {
        Title = title;
        Description = description;
        Sector = sector;
        Status = status;
        GoalValue = goalValue;
        CurrentValue = currentValue;
        PublicationDate = publicationDate;
        ClosingDate = closingDate;
        Progress = progress;
        UserId = userId;
    }

    public void Update(string title, string description, string sector, double goalValue, double currentValue, DateTime publicationDate, DateTime closingDate)
    {
        Title = title;
        Description = description;
        Sector = sector;
        GoalValue = goalValue;
        CurrentValue = currentValue;
        PublicationDate = publicationDate;
        ClosingDate = closingDate;
    }

}
