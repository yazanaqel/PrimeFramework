using Application.Features.User.RefreshToken;
using Application.Repositories;
using Domain.Abstractions;
using Domain.Entities.Users;
using Infrastructure.Abstractions;
using Infrastructure.Authentication.Enums;
using Infrastructure.Authentication.IdentityEntities;
using Infrastructure.Authentication.JwtSetup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity;

internal class UserIdentity(
    UserManager<User> userManager,
    ApplicationDbContext applicationDbContext,
    IJwtProvider jwtProvider,
    IOptions<JwtOptions> options,
    RefreshTokenGenerator refreshTokenGenerator) : IUserIdentity
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly JwtOptions _jwtOptions = options.Value;
    private readonly RefreshTokenGenerator _refreshTokenGenerator = refreshTokenGenerator;
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
    public async Task<AppUser?> GetAsync(ISpecification<AppUser> spec,CancellationToken cancellationToken = default)
    {
        var query = SpecificationEvaluator.GetQuery(_applicationDbContext.Set<AppUser>().AsQueryable(),spec);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<RefreshTokenResponse?> LoginAsync(string email,string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is not null && await _userManager.CheckPasswordAsync(user,password))
            return await GenerateTokensAsync(user);

        return null;
    }

    public async Task<RefreshTokenResponse?> RegisterAsync(AppUser appUser,CancellationToken cancellationToken)
    {
        var strategy = _applicationDbContext.Database.CreateExecutionStrategy();

        User user = new User { Id = appUser.Id,UserName = appUser.Email.Value,Email = appUser.Email.Value };

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);

            await _userManager.CreateAsync(user,appUser.Password);

            await userManager.AddToRoleAsync(user,nameof(Roles.USER));

            _applicationDbContext.Set<AppUser>().Add(appUser);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        });

        return await GenerateTokensAsync(user);
    }



    public async Task<RefreshTokenResponse?> RefreshTokenAsync(string accessToken,string refreshToken)
    {
        var principal = GetPrincipalFromExpiredToken(accessToken);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(userId);

        if(user == null ||
            user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return await GenerateTokensAsync(user);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken,tokenValidationParameters,out _);
        return principal;
    }
    private async Task<RefreshTokenResponse> GenerateTokensAsync(User user)
    {
        var accessToken = await _jwtProvider.GenerateAccessToken(user);
        var refreshToken = _refreshTokenGenerator.Generate();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenDays);

        await _userManager.UpdateAsync(user);

        return new RefreshTokenResponse(accessToken,refreshToken);

    }
}
