namespace VaquinhaOnline.Infrastructure.IdentityModels;

public class AppRole : IdentityRole<Guid>
{
    public ICollection<AppUserRole> UserRoles { get; set; } = null!;
    public AppRole() : base()
    {
    }

    public AppRole(string roleName) : base(roleName)
    {
        Name = roleName;
    }
}