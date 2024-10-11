using Domain.Constants;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.DatabaseSeed;
public class Seeder(IOptions<SeederOptions> options, UserManager<User> userManager) : ISeeder
{
    private readonly SeederOptions _options = options.Value;
    private readonly UserManager<User> _userManager = userManager;

    public async Task SeedPrimeUser()
    {
        if (_userManager.Users.All(u => u.UserName != _options.PrimeUsername))
        {
            var user = new User
            {
                UserName = _options.PrimeUsername,
                Email = _options.PrimeEmail,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, _options.PrimePassword);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, nameof(Roles.ADMIN));

        }
    }

    public async Task Initialize()
    {
        await SeedPrimeUser();
    }

}
