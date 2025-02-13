namespace VaquinhaOnline.Infrastructure.Seeds;

public class SeedService : ISeedService
{
    public async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

            await DbInitializer.SeedAsync(userManager, roleManager);
        }
    }
}
