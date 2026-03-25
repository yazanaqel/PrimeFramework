using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.DatabaseSeed;

public class Seeder(IOptions<SeederOptions> options,UserManager<User> userManager,RoleManager<Role> roleManager,ApplicationDbContext dbContext) : ISeeder
{
    private readonly SeederOptions _options = options.Value;
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task Initialize()
    {

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            //Admin Seed
            if(_userManager.Users.All(u => u.UserName != _options.PrimeUsername))
            {
                var user = new User
                {
                    UserName = _options.PrimeUsername,
                    Email = _options.PrimeEmail,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user,_options.PrimePassword);

                //Roles Seed
                foreach(var role in Enum.GetNames<Roles>())
                {
                    if(!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new Role
                        {
                            Id = Guid.NewGuid(),
                            Name = role,
                            NormalizedName = role.ToUpper(),
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                    }
                }

                //Admin Roles Seed
                List<string> roles = Enum.GetNames<Roles>().ToList();

                await _userManager.AddToRolesAsync(user,roles);

                IEnumerable<Permission> permissions = Enum.GetValues<Permissions>()
                        .Select
                        (permission =>

                            new Permission
                            {
                                PermissionName = permission.ToString().ToUpper(),
                                NormalizedName = permission.ToString().ToUpper()
                            }
                        ).ToList();

                _dbContext.Set<Permission>().AddRange(permissions);

                //Role Permission Seed
                var adminRole = await _roleManager.FindByNameAsync(nameof(Roles.ADMIN));

                if(adminRole is null)
                    return;

                var existing = _dbContext.Set<RolePermission>()
                            .Where(rp => rp.RoleId == adminRole.Id)
                            .ToList();

                foreach(var permission in Enum.GetValues<Permissions>())
                {
                    if(!existing.Any(e => e.Id == (int)permission))
                    {
                        _dbContext.Add(new RolePermission
                        {
                            RoleId = adminRole.Id,
                            Id = (int)permission,
                            ClaimValue = permission.ToString().ToUpper(),
                            ClaimType = permission.ToString(),
                        });
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            
            await transaction.CommitAsync();
        });

    }
}
