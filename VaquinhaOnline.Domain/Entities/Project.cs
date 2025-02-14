using VaquinhaOnline.Domain.DomainObjects;

namespace VaquinhaOnline.Domain.Entities;

public class Project : Entity
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Sector { get; private set; } = string.Empty;
    public string Status { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;   
    public double GoalValue { get; private set; }
    public double CurrentValue { get; private set; }
    public DateTime PublicationDate { get; private set; }
    public DateTime ClosingDate { get; private set; }


    //navegation properties
    public Guid UserId { get; set; }
    public Guid InvestmentId { get; set; }
    public virtual Investment Investment { get; set; }
    public Project()
    {
    }

    public Project(string title, string description, string sector, string image,double goalValue, DateTime closingDate, Guid userId)
    {
        Title = title;
        Description = description;
        Sector = sector;
        Status = "Criado";
        Image = image;
        GoalValue = goalValue;
        CurrentValue = 0;
        PublicationDate = DateTime.UtcNow;
        ClosingDate = closingDate;
        UserId = userId;
    }

    public void Update(string title, string description, string sector, double currentValue)
    {
        Title = title;
        Description = description;
        Sector = sector;
        CurrentValue = currentValue;
    }

    public void UpdateCurrentValue (double value)
    {
        CurrentValue += value;
    }

}
