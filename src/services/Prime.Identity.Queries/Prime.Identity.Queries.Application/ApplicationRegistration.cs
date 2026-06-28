using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Prime.Identity.Queries.Application.Features.User.Service;
using Prime.Identity.Queries.Application.Features.User.Service.Business;

namespace Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(AssemblyProvider.GetAssembly(),includeInternalTypes: true);

        services.AddScoped<IUserService,UserService>();
        services.AddScoped<ICategoryService,CategoryService>();
        services.AddScoped<IStoreService,StoreService>();


        return services;
    }
}
