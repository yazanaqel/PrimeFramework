using Domain.Constants;
using Domain.Shared;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.DatabaseSeed;
public class Seeder
{
    public static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (userManager.Users.All(u => u.UserName != DatabaseSeeder.PrimeUsername))
        {
            var user = new User
            {
                UserName = DatabaseSeeder.PrimeUsername,
                Email = DatabaseSeeder.PrimeEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, DatabaseSeeder.PrimePassword);

            if (result.Succeeded)
                await userManager.AddToRoleAsync(user, "admin");

        }
    }

}
