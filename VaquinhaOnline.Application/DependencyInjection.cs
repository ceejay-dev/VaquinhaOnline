using VaquinhaOnline.Application.Features.Projects;
using VaquinhaOnline.Application.Features.Users;

namespace VaquinhaOnline.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddService();

    private static IServiceCollection AddService(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract &&
            t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>))))
        {
            try
            {
                var validatorInterface = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));
                services.AddScoped(validatorInterface, type);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering {type.Name}: {ex.Message}");
            }
        }
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IInvestmentService, InvestmentService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}

