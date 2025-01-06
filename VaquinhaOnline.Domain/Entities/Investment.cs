using VaquinhaOnline.Domain.DomainObjects;

namespace VaquinhaOnline.Domain.Entities;

public class Investment : Entity
{
    public double ? Value { get; private set; }
    public DateTime InvestmentDate { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string InvestmentType { get; private set; } = string.Empty;

    //navegation properties
    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }    
    public Guid UserId { get; set; }

    public Investment()
    {
    }

    public Investment(double value, DateTime investmentDate, string description, string investmentType, Guid projectId, Guid userId)
    {
        Value = value;
        InvestmentDate = investmentDate;
        Description = description;
        InvestmentType = investmentType;
        ProjectId = projectId;
        UserId = userId;
    }

    public void Update(double value, string description, string investmentType)
    {
        Value = value;
        Description = description;
        InvestmentType = investmentType;
    }
}
