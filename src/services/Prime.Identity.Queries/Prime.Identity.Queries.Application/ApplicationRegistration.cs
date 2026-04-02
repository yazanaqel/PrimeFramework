using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(options => options.RegisterServicesFromAssemblies(
            AssemblyProvider.GetAssembly()));

        return services;
    }
}
