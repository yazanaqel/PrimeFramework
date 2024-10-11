using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.DatabaseSeed;
public class SeederOptionsSetup : IConfigureOptions<SeederOptions>
{
    private const string SectionName = Domain.Constants.DatabaseSeeder.PrimeAdmin;
    private readonly IConfiguration _configuration;

    public SeederOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(SeederOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}