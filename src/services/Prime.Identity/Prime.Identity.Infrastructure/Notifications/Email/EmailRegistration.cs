using Application.Notifications.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Notifications.Email;

internal static class EmailRegistration
{
    public static IServiceCollection AddEmail(this IServiceCollection services,IConfiguration configuration)
    {

        var emailSettings = configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>();

        services.AddFluentEmail(emailSettings.SenderEmail,emailSettings.SenderName)
            .AddSmtpSender(emailSettings.Host,emailSettings.Port,emailSettings.Username,emailSettings.Password);

        services.ConfigureOptions<EmailOptionsSetup>();

        services.AddScoped<IEmailService,EmailService>();

        return services;
    }
}
