using Application.Abstractions;
using Domain.Constants;
using Hangfire;
using Infrastructure.Jobs.HangfireAdapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Jobs;

internal static class JobsRegistrations
{
    public static IServiceCollection AddJobs(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString(AppSettingsSections.DefaultConnection));
        });

        services.AddHangfireServer();

        services.AddScoped<IJobScheduler,HangfireJobScheduler>();

        return services;
    }
}
