using VaquinhaOnline.Domain.Interfaces.IRepository;

namespace VaquinhaOnline.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    => services
            .AddDb(configuration)
            .AddRepositories();

    private static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Registrando o DbContext com PostgreSQL
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        //NpgsqlConnection.GlobalTypeMapper.UseJsonNet();

        // Registrando os serviços do Identity
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
        })
            .AddUserManager<UserManager<AppUser>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IInvestmentRepository, InvestmentRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();

        return services;
    }

    //private static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddScoped<IEmailService, EmailService>();

    //    return services;
    //}

}
