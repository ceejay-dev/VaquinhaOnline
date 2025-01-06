namespace VaquinhaOnline.Infrastructure.IdentityModels;

public class AppUserRole : IdentityUserRole<Guid>
{
    public AppRole Role { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}