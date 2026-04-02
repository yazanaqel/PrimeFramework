using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DatabaseSeed;

internal static class SeederRegistration
{
    public static IServiceCollection AddSeeder(this IServiceCollection services)
    {
        services.ConfigureOptions<SeederOptionsSetup>();

        services.AddScoped<ISeeder,Seeder>();

        return services;
    }
}
