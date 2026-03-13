using Application.Features.User.GetAllUsers;
using Application.Repositories;
using Domain.Abstractions;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.Authentication.JwtSetup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Identity;

internal class UserIdentity(UserManager<User> userManager,IJwtProvider jwtProvider,ApplicationDbContext applicationDbContext) : IUserIdentity
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task<bool> IsEmailAvailable(string email,CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null)
            return false;

        return true;
    }

    public async Task<IEnumerable<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken)
    {

        return await _applicationDbContext
            .Set<AppUser>()
            .AsNoTracking()
            //.Where(u => u.UserName.Contains("yaza"))
            .ToListAsync();
    }

    public async Task<string> LoginAsync(string email,string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null && await _userManager.CheckPasswordAsync(user,password))
            return await _jwtProvider.GenerateAsync(user);

        return "Wrong Email Or Password";
    }

    public async Task<string> RegisterAsync(AppUser appUser,CancellationToken cancellationToken)
    {
        var strategy = _applicationDbContext.Database.CreateExecutionStrategy();

        string token = string.Empty;

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);

            var user = new User { Id = appUser.Id,UserName = appUser.Email.Value,Email = appUser.Email.Value };

            await _userManager.CreateAsync(user,appUser.Password);

            await userManager.AddToRoleAsync(user,nameof(Roles.USER));

            token = await _jwtProvider.GenerateAsync(user);

            _applicationDbContext.Set<AppUser>().Add(appUser);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        });

        return token;
    }


    public async Task<AppUser?> GetAsync(ISpecification<AppUser> spec,CancellationToken cancellationToken = default)
    {
        var query = SpecificationEvaluator.GetQuery(_applicationDbContext.Set<AppUser>().AsQueryable(),spec);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

}
