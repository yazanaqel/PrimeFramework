using Domain.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Notifications.Email;

public class EmailOptionsSetup(IConfiguration configuration) : IConfigureOptions<EmailSettings>
{
    private const string SectionName = AppSettingsSections.EmailSettings;
    private readonly IConfiguration _configuration = configuration;
    public void Configure(EmailSettings options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}