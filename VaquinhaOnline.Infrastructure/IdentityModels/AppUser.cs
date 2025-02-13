namespace VaquinhaOnline.Infrastructure.IdentityModels;

public class AppUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string ProfilePhoto { get; set; } = string.Empty;    
    public DateTime CreationDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    // navegations properties 
    public List<Project> Projects { get; set; } = new List<Project>();
    public List<Investment> Investments { get; set; } = new List<Investment>();
    public ICollection<AppUserRole> UserRoles { get; set; } = null!;
}