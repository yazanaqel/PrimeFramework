using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Prime.Identity.Application.Behaviors;

namespace Application;
public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(options => options.RegisterServicesFromAssemblies(
            AssemblyProvider.GetAssembly()));

        services.AddValidatorsFromAssembly(AssemblyProvider.GetAssembly(),includeInternalTypes: true);


        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnitOfWorkBehavior<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>),typeof(CurrentUserBehavior<,>));

        return services;
    }
}
