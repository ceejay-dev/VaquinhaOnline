namespace VaquinhaOnline.Infrastructure.Seeds;

public interface ISeedService
{
    Task SeedDatabaseAsync(IServiceProvider serviceProvider);
}
