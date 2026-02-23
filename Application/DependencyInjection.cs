using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(options => options.RegisterServicesFromAssemblies(
            AssemblyProvider.GetAssembly()));

        services.AddValidatorsFromAssembly(AssemblyProvider.GetAssembly(),includeInternalTypes: true);


        services.AddScoped(typeof(IPipelineBehavior<,>),typeof(LoggingBehavior<,>));
        //services.AddScoped(typeof(IPipelineBehavior<,>),typeof(UnitOfWorkBehavior<,>));

        return services;
    }
}
