namespace VaquinhaOnline.Infrastructure.IdentityModels;

public class AppRole : IdentityRole<Guid>
{
    public ICollection<AppUserRole> UserRoles { get; set; } = null!;
}